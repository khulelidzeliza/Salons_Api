using RazorLight;
using Salons_Project.Application.Common.Interfaces;

namespace Salons_Project.Infrastructure.Smtp.Templates
{
    public class EmailTemplateService : IEmailTemplatesService
    {
       private readonly RazorLightEngine _razorEngine;

        public EmailTemplateService()
        {
            _razorEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Emails"))
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> GenerateEmailAsync<T>(string templateName, T model)
        {
            string template = await _razorEngine.CompileRenderAsync(templateName, model);

            return template;
        }
    }
}
