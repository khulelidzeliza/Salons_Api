

using Salons_Project.Domain.Common;

namespace Salons_Project.Domain.Models.Content;

public class Favorite : BaseEntity
{
    public int UserId { get; set; }
    public int SalonId { get; set; }
}
