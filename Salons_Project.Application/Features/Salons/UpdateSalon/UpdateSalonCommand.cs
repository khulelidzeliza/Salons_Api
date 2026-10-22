using MediatR;
using Microsoft.AspNetCore.Http;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Application.Features.Salons.UpdateSalon;

public sealed record UpdateSalonCommand(
    int UserId,
    BudgetLevel Price,
    string? FacebookLink,
    string? InstagramLink,
    List<IFormFile>? Pictures,
    string GeorgianName,
    string EnglishName,
    string Email,
    string Location,
    string PhoneNumber,
    string WorkingHours
) : IRequest<Result>;