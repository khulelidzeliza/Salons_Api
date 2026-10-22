using MediatR;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Features.Organization.DeleteSalon;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Application.Features.Organization.DeleteSalon;
    public class DeleteSalonnHandler : IRequestHandler<DeleteSalonCommand , Result>
    {
        private readonly IDataContext _db;
        private IRequestHandler<DeleteSalonCommand, Result> _requestHandlerImplementation;

        public DeleteSalonnHandler(IDataContext db) => _db = db;

        public async Task<Result> Handle(DeleteSalonCommand request, CancellationToken cancellationToken)
        {
            var salonexists = _db.Salons.FirstOrDefault(s => request.SalonId == s.Id 
                                        && request.userid == s.UserId);
            
            if(salonexists == null)  return Result.Failure(Errors.NotFound("Salon not found."));

            _db.Salons.Remove(salonexists);
            await _db.SaveChangesAsync();
            return Result.Success();
            
        }
    }
