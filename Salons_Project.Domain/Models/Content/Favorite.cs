

using Salons_Project.Domain.Common;
using Salons_Project.Domain.Models.Organization;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Domain.Models.Content;

public class Favorite : BaseEntity
{
    public User User { get; set; }
    public int UserId { get; set; }
    public Salon Salon { get; set; }
    public int SalonId { get; set; }
}
