using Salons_Project.Domain.Models.Organization;
using MediatR;

namespace Salons_Project.Application.Features.Organization.Queries.GetOrganization;
public sealed record GetSalonByIdQueryable(int Id) : IRequest<IQueryable<Salon>>;
