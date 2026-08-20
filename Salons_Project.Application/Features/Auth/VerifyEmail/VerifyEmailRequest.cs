



namespace Salons_Project.Application.Features.Auth.VerifyEmail;

public class VerifyEmailRequest
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
