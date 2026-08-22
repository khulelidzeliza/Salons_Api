using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Application.Features.Organization.CreateOrganization;

public class CreateSalonRequest
{
    public int UserId { get; set; }
    public BudgetLevel Price { get; set; }
    public string? FacebookLink { get; set; }
    public string? InstagramLink { get; set; }
    public List<string>? Pictures { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string WorkingHours { get; set; } = string.Empty;
}
