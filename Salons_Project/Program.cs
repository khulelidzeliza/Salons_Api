using Microsoft.Extensions.FileProviders;
using Salons_Project.Api.Extensions.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServer (builder.Configuration);

var app = builder.Build();

app.UseApplication();

var uploadsPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "uploads");
Directory.CreateDirectory(uploadsPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

app.Run();



