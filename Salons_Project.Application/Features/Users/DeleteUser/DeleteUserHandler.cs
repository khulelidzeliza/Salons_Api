using MediatR;
using Microsoft.EntityFrameworkCore;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Users.DeleteUser;
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand , Result>
    {
        private readonly IDataContext _db;
        public DeleteUserHandler(IDataContext db) => _db = db;
       
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == request.userId, cancellationToken);

            if (user is null)
                return Result.Failure(Errors.DoesNotExist()); 

            _db.Users.Remove(user);
            await _db.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

