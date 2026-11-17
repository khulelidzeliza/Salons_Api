using Salons_Project.Domain.Models.Organization;
using HotChocolate.Data.Filters;

namespace Salons_Project.Api.Projection.graphql.procedures;

public class ProcedureType  : ObjectType<Procedure>
{
    protected override void Configure(IObjectTypeDescriptor<Procedure> descriptor)
    {
        descriptor.Field(s => s.Id);
        descriptor.Field(s => s.Masters);
        descriptor.Field(s => s.ProcedureType);
        descriptor.Field(s => s.FinishedWorks);

        
    }
}