using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace Salons_Project.Api.Middleware;
public sealed class ExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (ValidationException ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            ctx.Response.ContentType = "application/problem+json";


            var errors = ex.Errors
                              .GroupBy(e => string.IsNullOrWhiteSpace(e.PropertyName) ? "General" : e.PropertyName)
                              .ToDictionary(
                                  g => g.Key,
                                  g => g.Select(e => e.ErrorMessage).ToArray()
                              );

            ValidationProblemDetails details = new ValidationProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validation error",
                Detail = ex.Message,
                Errors = errors,
                Status = 400,
            };

            await ctx.Response.WriteAsJsonAsync(details);
        }
    }
}

public static class ExceptionExtensions
{
    public static IApplicationBuilder UseValidationProblemDetails(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}