using Salons_Project.Domain.Common;
using Salons_Project.Domain.Enums.Organization;
using Salons_Project.Domain.Models.Content;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Domain.Models.Organization;

public class Salon : BaseEntity
{
    public string GeorgianName { get; set; }
    public string EnglishName { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public string PhoneNumber { get; set; }
    public string WorkingHours { get; set; }
    public string? FacebookLink { get; set; }
    public string? InstagramLink { get; set; }
    public BudgetLevel Price { get; set; } = BudgetLevel.Medium;

    public int UserId { get; set; }
    public User User { get; set; }
    public List<Review> Reviews { get; set; } = new();  
    public List<string> Pictures { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
    public List<Procedure> Procedures { get; set; } = new();
}
