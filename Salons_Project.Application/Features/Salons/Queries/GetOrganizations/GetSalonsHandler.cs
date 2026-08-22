
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Models.Organization;
using MediatR;

namespace Salons_Project.Application.Features.Organization.Queries.GetOrganizations;

public sealed class GetSalonsHandler : IRequestHandler<GetSalonsQueryable, IQueryable<Salon>>
{
    private readonly IDataContext _db;

    public GetSalonsHandler(IDataContext db) => _db = db;


    public Task<IQueryable<Salon>> Handle(GetSalonsQueryable req, CancellationToken cancellationToken)
    {
        IQueryable<Salon> query = _db.Salons.OrderByDescending(s => s.CreatedAt).ToList().AsQueryable();

        return Task.FromResult(query);
    }
}
