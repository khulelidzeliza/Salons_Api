



using MediatR;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Auth.Service;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Salons_Project.Application.Features.Auth.Register;


public class RegisterHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IDataContext _db;
    private readonly IAuthEmailService _email;

    public RegisterHandler(IDataContext db, IAuthEmailService email)
    {
        _db = db;
        _email = email;
    }
    public async Task<Result> Handle(RegisterCommand req, CancellationToken ct)
    {
        var emailTaken = await _db.Users
        .AnyAsync(u => u.Email.Value == req.Email, ct);

        if (emailTaken)
            return Result.Failure(Errors.AlreadyExists());
        var userResult = User.Create(req.Username, req.Email);
        if (!userResult.IsSuccess) return Result.Failure(userResult.Error!);

        var user = userResult.Value!;
        user.SetPassword(BCrypt.Net.BCrypt.HashPassword(req.Password));

        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        var verification = await _email.SendEmailVerification(user);

        verification.SetUserId(user.Id);

        _db.EmailVerifications.Add(verification);
        await _db.SaveChangesAsync(ct);

        user.SetEmailVerification(verification);

        return Result.Success();
    }


}
