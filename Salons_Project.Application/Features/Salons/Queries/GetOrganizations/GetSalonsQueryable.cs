using MediatR;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Application.Features.Organization.Queries.GetOrganizations;

public sealed record GetSalonsQueryable() : IRequest<IQueryable<Salon>>;
