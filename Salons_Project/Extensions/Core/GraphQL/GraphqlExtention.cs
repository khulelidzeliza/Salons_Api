using Salons_Project.Api.Filters;
using Salons_Project.Api.Projection.graphql.salons;
using Salons_Project.Api.Projection.graphql.users;

namespace Salons_Project.Api.Extensions.Core.GraphQL;

public static class GraphqlExtention
{
        public static IServiceCollection AddGraphqlConfiguration(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
                .ModifyPagingOptions(opt => opt.IncludeTotalCount = true)
                .AddErrorFilter<GraphQlErrorFilter>()
                .AddPagingArguments()
                .ModifyCostOptions(options => options.MaxFieldCost = 10_000)
                .AddAuthorization()
                //.AddMutationType()
                .AddProjections()
                .AddFiltering()
                .AddQueryType()
                .AddSorting()
                .AddTypeExtension<UserQueries>()
                .AddTypeExtension<SalonQueries>()
                .AddType<UserType>()
                .AddType<SalonType>()
            ;


            return services;
        }
    }
