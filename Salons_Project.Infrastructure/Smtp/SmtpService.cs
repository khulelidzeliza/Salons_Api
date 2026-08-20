using Salons_Project.Application.Common.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Salons_Project.Infrastructure.Smtp;

    public class SmtpService  : ISmtpService
{
        private readonly string _email = "khulelidzeliza@gmail.com";
        private readonly string _password = "mmex kcep mhtd habh";

        public async Task SendEmail(string subject, string body, string email)
        {
            using var mail = new MailMessage();
            mail.From = new MailAddress(_email, "Instagram");
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.To.Add(email);
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_email, _password),
            };

            await smtpClient.SendMailAsync(mail);
        }
}
