using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Contracts;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;
using System.ComponentModel.DataAnnotations;

namespace Salons_Project.Application.Features.Auth.VerifyEmail;

public sealed class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, Result<TokenResponse>>
{
    private readonly IDataContext _db;
    private readonly IJwtService _jwt;

    public VerifyEmailHandler(
        IDataContext db,
        IJwtService jwt
    )
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<Result<TokenResponse>> Handle(VerifyEmailCommand req, CancellationToken ct)
    {
        var user = await _db.Users
            .Include(u => u.EmailVerification)
            .FirstOrDefaultAsync(u => u.Email.Value == req.email, ct);

        if (user == null)
            throw new ValidationException("Unable to complete email verification. Please try again.");

        if (user.IsVerified)
            return Result<TokenResponse>.Failure(Errors.Invalid("This account has already been verified."));

        if (user.EmailVerification.Attempts >= 3)
        {
            return Result<TokenResponse>.Failure(Errors.Invalid(
                "You have reached the maximum number of verification attempts. Please req a new code."));
        }

        if (user.EmailVerification.Code != req.code)
        {
            user.EmailVerification.IncrementAttempts();
            await _db.SaveChangesAsync(ct);

            return Result<TokenResponse>.Failure(Errors.Invalid("The verification code you entered is incorrect."));
        }

        if (user.EmailVerification.ExpiredAt < DateTime.UtcNow)
        {
            return Result<TokenResponse>.Failure(
                Errors.Invalid("Your verification code has expired. Please req a new one."));
        }

        user.EmailVerification.DeleteCode();
        user.Verify();

        await _db.SaveChangesAsync(ct);

        var refreshToken = _jwt.GeneratePasswordResetToken(user);
        var accessToken = _jwt.GenerateAccessToken(user);

        return Result<TokenResponse>.Success(new TokenResponse(accessToken));
    }
}
