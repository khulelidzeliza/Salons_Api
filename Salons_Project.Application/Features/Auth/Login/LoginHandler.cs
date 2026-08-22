using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Auth.Service;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Auth.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IDataContext _db;
    private readonly IJwtService _jwt;
    private readonly IAuthEmailService _email;

    public LoginHandler(
        IDataContext db,
        IJwtService jwt,
        IAuthEmailService email
    )
    {
        _db = db;
        _jwt = jwt;
        _email = email;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand req, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking()
            .Where(e => e.Email.Value == req.Email)
            .FirstOrDefaultAsync(ct);

        if (user is null)
            return Result<TokenResponse>.Failure(Errors.InvalidCredentials());

        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
            return Result<TokenResponse>.Failure(Errors.InvalidCredentials());

        if (!user.IsVerified)
        {
            var verification = await _email.SendEmailVerification(user);
            verification.SetUserId(user.Id);

            var verificationExits = await _db.EmailVerifications
                .FirstOrDefaultAsync(e => e.UserId == user.Id, ct);

            if (verificationExits is not null) _db.EmailVerifications.Remove(verificationExits);

            _db.EmailVerifications.Add(verification);
            await _db.SaveChangesAsync(ct);

            return Result<TokenResponse>.Success(new TokenResponse("Verification"));
        }

        string accessToken = _jwt.GenerateAccessToken(user);

        return Result<TokenResponse>.Success(new TokenResponse(accessToken));
    }
}
