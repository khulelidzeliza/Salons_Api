using Salons_Project.Domain.Common;
using Salons_Project.Domain.Enums.User;
using Salons_Project.Domain.Models.Content;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Domain.Models.User;

public class User : BaseEntity
{
    public int Age { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? PictureUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsVerified { get; set; } = false;
    public Roles Role { get; set; } = Roles.User;
    public Genders Gender { get; set; } = Genders.Male;

    public List<Salon> Salons { get; set; } = new();
    public List<Review> Reviews { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();


}
