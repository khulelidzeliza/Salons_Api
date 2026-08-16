using Salons_Project.Domain.Common;
using Salons_Project.Domain.Enums.Content;

namespace Salons_Project.Domain.Models.Content;

public class Review : BaseEntity
{
    public Star star { get; set; } = Star.Three;
    public string? Comment { get; set; }

}
