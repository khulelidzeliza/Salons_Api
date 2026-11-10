using Salons_Project.Application.Common.Contracts;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GeneratePasswordResetToken(User user);
    PasswordResetClaims? ReadPasswordResetToken(string token);
    
}
