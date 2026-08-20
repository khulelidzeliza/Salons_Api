



using Salons_Project.Domain.Common.ValueObjects.Users;

namespace Salons_Project.Application.Features.Auth.Login;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; }
}
