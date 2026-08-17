using Salons_Project.Domain.Models.Auth;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Application.Features.Auth.Service;

public interface IAuthEmailService
{
    Task<EmailVerification> SendEmailVerification(User user);
    Task<PasswordVerification> SendPasswordVerification(User user);
}
