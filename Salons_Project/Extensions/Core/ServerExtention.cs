using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Api.Extensions.Core.GraphQL;
using Salons_Project.Application.Features.Auth.Register;
using Salons_Project.Infrastructure;
using Salons_Project.Application.Common.Behaviors;
using Salons_Project.Api.Filters;
using MediatR;
using FluentValidation;


namespace Salons_Project.Api.Extensions.Core;

public static class ServerExtensions
{
    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddControllers(o => o.Filters.Add<ResultFilter>());
        services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
        services.AddInfrastructure(config);
        services.AddGraphqlConfiguration();
        services.AddHttpContextAccessor();
        services.AddApplication();

        services.AddSignalR();
        services.AddEndpointsApiExplorer();
        services.AddJwtAuth(config);
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddServerCors();

        return services;
    }

    private static IServiceCollection AddServerCors(this IServiceCollection services)
    {
        services.AddCors(cors =>
        {
            cors.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}
