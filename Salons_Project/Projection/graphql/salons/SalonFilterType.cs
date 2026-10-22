using HotChocolate.Data.Filters;
using Salons_Project.Api.Projection.graphql.procedures;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Api.Projection.graphql.salons;

public class SalonFilterType : FilterInputType<Salon>
{
    protected override void Configure(IFilterInputTypeDescriptor<Salon> descriptor)
    {
        descriptor.Field(s => s.GeorgianName);
        descriptor.Field(s => s.EnglishName);
        descriptor.Field(s => s.Location);
        descriptor.Field(s => s.Price);

        descriptor.Field(s => s.Procedures)
            .Type<ListFilterInputType<ProcedureFilterType>>();
    }
}