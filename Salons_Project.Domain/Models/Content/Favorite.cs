

using Salons_Project.Domain.Common;

namespace Salons_Project.Domain.Models.Content;

public class Favorite : BaseEntity
{
    public string UserId { get; set; }
    public string SalonId { get; set; }
}
