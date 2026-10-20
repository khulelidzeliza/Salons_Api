using MediatR;
using Microsoft.AspNetCore.Authorization;
using Salons_Project.Application.Features.Organization.Queries.GetOrganization;
using Salons_Project.Application.Features.Organization.Queries.GetOrganizations;
using Salons_Project.Application.Features.Salons.Queries.GetMySalons;
using Salons_Project.Domain.Models.Organization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HotChocolate.CostAnalysis.Types;

namespace Salons_Project.Api.Projection.graphql.salons
{
    [QueryType]
    public class SalonQueries
    {
        [UseProjection]
        [Authorize]
        public async Task<IQueryable<Salon>> getMySalons(
            IHttpContextAccessor http,
            IMediator mediator,
            CancellationToken ct)
        {
            var userIdClaim = http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                throw new GraphQLException("User not found!");

            if (!int.TryParse(userIdClaim, out int userId))
                throw new ValidationException("User ID claim is not a valid integer.");

            return await mediator.Send(new GetMySalonsQueryable(userId), ct);

        }

        [UseProjection]
        [UseSorting]
        [UseFiltering(typeof(SalonFilterType))]
        [ListSize(AssumedSize = 50)]
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
