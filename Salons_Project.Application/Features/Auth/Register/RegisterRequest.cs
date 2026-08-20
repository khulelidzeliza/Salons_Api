using Salons_Project.Domain.Common.ValueObjects.Users;

namespace Salons_Project.Application.Features.Auth.Register;

public class RegisterRequest
{
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; } 

}
