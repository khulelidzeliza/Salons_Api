using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Salons_Project.Infrastructure.Auth.Jwt;
using Salons_Project.Infrastructure.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Salons_Project.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>((sp, opts) => { opts.UseSqlServer(config.GetConnectionString("Default")); });

        services.AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());

        var jwt = config.GetSection("Jwt").Get<JwtSettings>()!;
        services.AddSingleton(jwt);

        services.AddScoped<IEmailTemplatesService, EmailTemplateService>();
        services.AddScoped<ISmtpService , SmtpService>();
        services.AddScoped<IJwtService, JwtService>();
        return services;
    }
}