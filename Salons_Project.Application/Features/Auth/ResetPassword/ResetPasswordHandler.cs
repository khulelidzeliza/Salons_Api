using Salons_Project.Application.Features.Auth.Service;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Salons_Project.Application.Features.Auth.ResetPassword;

public sealed class ResetPasswordHandler
    : IRequestHandler<ResetPasswordCommand, Result<bool>>
{
    private readonly IDataContext _dataContext;
    private readonly IAuthEmailService _emailSender;

    public ResetPasswordHandler(
        IDataContext dataContext,
        IAuthEmailService emailSender
       )
    {
        _dataContext = dataContext;
        _emailSender = emailSender;
    }

    public async Task<Result<bool>> Handle(
        ResetPasswordCommand req,
        CancellationToken cancellationToken)
    {
        var user = await _dataContext.Users
            .Include(u => u.PasswordEmailVerification)
            .FirstOrDefaultAsync(u => u.Email.Value == req.Email, cancellationToken);

        if (user is null)
        {
            return Result<bool>.Failure(Errors.NotFound("User could not be found"));
        }

        if (user.PasswordEmailVerification?.Attempts >= 3)
        {
            return Result<bool>.Failure(Errors.Invalid("Maximum verification attempts reached"));
        }

        var existingVerification = await _dataContext.PasswordVerifications
            .FirstOrDefaultAsync(e => e.UserId == user.Id, cancellationToken);

        if (existingVerification is not null)
            _dataContext.PasswordVerifications.Remove(existingVerification);


        var passwordResetVerification = await _emailSender.SendPasswordVerification(user);
        passwordResetVerification.SetUserId(user.Id);

        if (user.PasswordEmailVerification is not null)
        {
            user.PasswordEmailVerification.IncrementAttempts();
        }

        _dataContext.PasswordVerifications.Add(passwordResetVerification);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}