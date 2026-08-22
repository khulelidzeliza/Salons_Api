using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Auth.Service;
using Salons_Project.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salons_Project.Application.Features.Auth.ResendVerificationCode
{
    public class ResendVerificationHandler : IRequestHandler<ResendVerificationCommand, Result<bool>>
    {
        private readonly IDataContext _db;
        private readonly IAuthEmailService _email;

        public ResendVerificationHandler(
            IDataContext db,
            IAuthEmailService email
        )
        {
            _db = db;
            _email = email;
        }

        public async Task<Result<bool>> Handle(ResendVerificationCommand req, CancellationToken ct)
        {
            var user = await _db.Users
                .Where(e => e.Email.Value == req.Email)
                .FirstOrDefaultAsync(ct);

            if (user is null)
                return Result<bool>.Failure(Errors.NotFound(req.Email));

            if (user.IsVerified)
                return Result<bool>.Failure(Errors.AlreadyVerified());

            var verification = await _email.SendEmailVerification(user);
            verification.SetUserId(user.Id);

            var verificationExits = await _db.EmailVerifications
                .FirstOrDefaultAsync(e => e.UserId == user.Id, ct);

            if (verificationExits is not null)
                _db.EmailVerifications.Remove(verificationExits);

            _db.EmailVerifications.Add(verification);
            await _db.SaveChangesAsync(ct);

            return Result<bool>.Success(true);
        }
    }
}
