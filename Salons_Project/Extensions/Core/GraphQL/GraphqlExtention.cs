using Salons_Project.Api.Controllers.graphql.users;
using Salons_Project.Api.Filters;

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
                .AddAuthorization()
                //.AddMutationType()
                .AddProjections()
                .AddFiltering()
                .AddQueryType()
                .AddSorting()
                .AddTypeExtension<UserQueries>()
                .AddType<UserType>()
            ;


            return services;
        }
    }
