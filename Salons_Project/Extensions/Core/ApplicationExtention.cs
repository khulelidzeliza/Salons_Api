using Microsoft.Extensions.FileProviders;
using Salons_Project.Api.Extensions.Auth;
using Salons_Project.Api.Middleware;

namespace Salons_Project.Api.Extensions.Core;
public static class ApplicationExtensions
{
    public static WebApplication UseApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        

        app.UseCors();

        app.UseValidationProblemDetails();

        app.UseHttpsRedirection();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
               System.IO.Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
            RequestPath = "/uploads"
        });
        app.UseJwtAuth();

        app.MapControllers();

        app.MapGraphQL("/graphql");

        return app;
    }
}
