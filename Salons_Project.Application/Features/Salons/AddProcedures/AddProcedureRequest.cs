using Salons_Project.Domain.Enums.Organization;


namespace Salons_Project.Application.Features.Salons.AddProcedures;

public class AddProcedureRequest
{
    public ProcedureType ProcedureType { get; set; } = ProcedureType.Hair;
    public List<string> Masters { get; set; }
    public List<string> FinishedWorks { get; set; }
    public int SalonId { get; set; }
}
