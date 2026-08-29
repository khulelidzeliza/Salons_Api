using MediatR;
using Microsoft.AspNetCore.Http;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Application.Features.Organization.CreateOrganization;
public sealed record CreateSalonCommand(
    string Name,
    string Location,
    string PhoneNumber,
    string Email,
    BudgetLevel Price,
    string? FacebookLink,
    string? InstagramLink,
    List<IFormFile>? Pictures,
    int UserId,
    string workingHours
) : IRequest<Result>;