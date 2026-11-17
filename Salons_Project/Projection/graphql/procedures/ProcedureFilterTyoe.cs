using HotChocolate.Data.Filters;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Api.Projection.graphql.procedures;

public class ProcedureFilterType : FilterInputType<Procedure>
{
    protected override void Configure(IFilterInputTypeDescriptor<Procedure> descriptor)
    {
        descriptor.Field(p => p.ProcedureType);
        descriptor.Field(s => s.Masters);
        descriptor.Field(s => s.FinishedWorks);
        
        // Masters / FinishedWorks are List<string> — only add these if you
        // actually want to filter procedures by master name or portfolio text.
        // Probably not needed for the search bar you showed earlier.
    }
}