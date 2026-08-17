using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GeneratePasswordResetToken(User user);
}
