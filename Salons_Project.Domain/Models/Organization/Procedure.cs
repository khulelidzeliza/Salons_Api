
using Salons_Project.Domain.Common;
using Salons_Project.Domain.Enums.Organization;

namespace Salons_Project.Domain.Models.Organization;

public class Procedure : BaseEntity
{
    public ProcedureType ProcedureType { get; set; } = ProcedureType.Hair;
    public List<string> Masters { get; set; }
    public List<string> FinishedWorks { get; set; }
    public Salon Salon { get; set; }
    public int SalonId { get; set; }

}
