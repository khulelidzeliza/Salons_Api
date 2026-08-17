using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Models.Organization;
using MediatR;

namespace Salons_Project.Application.Features.Organization.Queries.GetOrganization;

internal class GetSalonByIdHandler : IRequestHandler<GetSalonByIdQueryable, IQueryable<Salon>>
{
    private readonly IDataContext _db;

    public GetSalonByIdHandler(IDataContext db) => _db = db;

    public Task<IQueryable<Salon>> Handle(GetSalonByIdQueryable req, CancellationToken cancellationToken)
    {
        IQueryable<Salon> query = _db.Salons.Where(e => e.Id == req.Id);

        return Task.FromResult(query);
    }
}
