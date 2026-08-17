namespace Salons_Project.Application.Common.Interfaces;

public interface IEmailTemplatesService
{
    Task<string> GenerateEmailAsync<T>(string templateName, T model);

}
