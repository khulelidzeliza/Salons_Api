



namespace Salons_Project.Application.Common.Interfaces
{
    public interface ISmtpService
    {
        Task SendEmail(string subject, string body, string email);

    }
}
