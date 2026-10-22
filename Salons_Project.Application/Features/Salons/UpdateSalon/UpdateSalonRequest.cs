using Microsoft.AspNetCore.Http;
using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Application.Features.Salons.UpdateSalon;

public class UpdateSalonRequest
{
    public int UserId { get; set; }
    public BudgetLevel Price { get; set; }
    public string? FacebookLink { get; set; }
    public string? InstagramLink { get; set; }
    public string GeorgianName { get; set; }
    public string EnglishName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string WorkingHours { get; set; } = string.Empty;
}