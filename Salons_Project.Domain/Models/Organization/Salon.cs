using Salons_Project.Domain.Common;
using Salons_Project.Domain.Enums.Organization;
using Salons_Project.Domain.Models.Content;

namespace Salons_Project.Domain.Models.Organization;

public class Salon : BaseEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string PhoneNumeber { get; set; }
    public string Email { get; set; }
    public BudgetLevel Price { get; set; } = BudgetLevel.Medium;
    //public string WorkingHours { get; set; }
    public string FacebookLink { get; set; }
    public string InstagramLink { get; set; }

    public bool IsFavorited { get; set; }
   public int UserId { get; set; }
    public User User { get; set; }
    public List<string> Pictures { get; set; } = new();

    public List<Review> Reviews { get; set; } = new();
   
}
