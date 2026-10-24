using Salons_Project.Domain.Enums.Organization;
using Salons_Project.Domain.Common.Results;
using MediatR;

namespace Salons_Project.Application.Features.Salons.UpdateSalon;

public sealed record UpdateSalonCommand(
    int UserId,
    BudgetLevel Price,
    string? FacebookLink,
    string? InstagramLink,
    string GeorgianName,
    string EnglishName,
    string? Email,
    string? Description,
    string Location,
    string PhoneNumber,
    string WorkingHours
) : IRequest<Result>;