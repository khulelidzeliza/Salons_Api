
using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Users.UpdateUser;


    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IDataContext _db;

        public UpdateUserHandler(IDataContext db) => _db = db;

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null)
                return Result.Failure(Errors.NotFound("user not found")); 

            user.Age = request.Age;
            user.UserName = request.UserName;
            user.PhoneNumber = request.PhoneNumber;
            user.City = request.City;

            await _db.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
