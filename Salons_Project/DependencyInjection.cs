using Salons_Project.Application.Features.Auth.Service;

namespace Salons_Project.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthEmailService, AuthEmailService>();

        return services;
    }
}
