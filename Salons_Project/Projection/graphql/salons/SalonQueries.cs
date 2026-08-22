using MediatR;
using Salons_Project.Application.Features.Organization.Queries.GetOrganization;
using Salons_Project.Application.Features.Organization.Queries.GetOrganizations;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Api.Projection.graphql.salons
{
    [QueryType]
    public class SalonQueries
    {
        [UseProjection]
        public async Task<IQueryable<Salon>> getSalons(
            IMediator mediator,
            CancellationToken ct)
        {
            return await mediator.Send(new GetSalonsQueryable(), ct);
        }
        [UseProjection]
        public async Task<IQueryable<Salon>> getSalonById(
            int id,
            IMediator mediator,
            CancellationToken ct)
        {
            return await mediator.Send(new GetSalonByIdQueryable(id), ct);
        }
    }
}
