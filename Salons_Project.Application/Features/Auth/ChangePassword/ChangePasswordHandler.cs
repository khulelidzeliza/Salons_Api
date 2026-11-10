using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Auth.ChangePassword;

public sealed class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    private readonly IDataContext _db;
    private readonly IJwtService _jwt;

    public ChangePasswordHandler(IDataContext db, IJwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<Result<bool>> Handle(ChangePasswordCommand req, CancellationToken ct)
    {
        // 1. Token: signature, issuer, audience, 30-min lifetime, "pwd_reset" claim
        var claims = _jwt.ReadPasswordResetToken(req.Token);
        if (claims is null)
            return Result<bool>.Failure(Errors.LinkExpired());

        // 2. Password rules (same as register: 8–50 chars)
        if (string.IsNullOrWhiteSpace(req.NewPassword) || req.NewPassword.Length < 8 || req.NewPassword.Length > 50)
            return Result<bool>.Failure(Errors.Invalid("Password must be between 8 and 50 characters."));

        if (req.NewPassword != req.ConfirmPassword)
            return Result<bool>.Failure(Errors.Invalid("Passwords do not match."));

        // 3. User
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == claims.UserId, ct);
        if (user is null)
            return Result<bool>.Failure(Errors.LinkExpired());

        // 4. One-time use: the code inside the token must match the latest stored code.
        //    Used links (code deleted) and older links (a newer one was requested) are rejected.
        var verification = await _db.PasswordVerifications
            .FirstOrDefaultAsync(v => v.UserId == user.Id, ct);

        if (verification is null || string.IsNullOrEmpty(verification.Code) || verification.Code != claims.Code)
            return Result<bool>.Failure(Errors.LinkExpired());

        if (BCrypt.Net.BCrypt.Verify(req.NewPassword, user.Password))
            return Result<bool>.Failure(Errors.Invalid("New password must be different from your current one."));

        // 5. Save
        user.SetPassword(BCrypt.Net.BCrypt.HashPassword(req.NewPassword));
        _db.PasswordVerifications.Remove(verification);

        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}
