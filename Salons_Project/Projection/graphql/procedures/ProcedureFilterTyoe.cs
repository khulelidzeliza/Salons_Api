using HotChocolate.Data.Filters;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Api.Projection.graphql.procedures;

public class ProcedureFilterType : FilterInputType<Procedure>
{
    protected override void Configure(IFilterInputTypeDescriptor<Procedure> descriptor)
    {
        descriptor.Field(p => p.ProcedureType);
        // Masters / FinishedWorks are List<string> — only add these if you
        // actually want to filter procedures by master name or portfolio text.
        // Probably not needed for the search bar you showed earlier.
    }
}