using Salons_Project.Domain.Models.Auth;
using Salons_Project.Domain.Models.Users;
using Microsoft.Extensions.Configuration;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Application.Emails;

namespace Salons_Project.Application.Features.Auth.Service;


public sealed class AuthEmailService : IAuthEmailService
{
    private readonly IEmailTemplatesService _template;
    private readonly IConfiguration _config;
    private readonly ISmtpService _email;
    private readonly IJwtService _jwtTokenService;

    public AuthEmailService(
        IConfiguration config,
        ISmtpService email,
        IJwtService jwtTokenService,
        IEmailTemplatesService template
    )
    {
        _template = template;
        _config = config;
        _email = email;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<EmailVerification> SendEmailVerification(User user)
    {
        var verification = EmailVerification.Create();


        var verificationLink = $"{_config["App:FrontendUrl"]}/auth/verification/{user.Email.Value}/{verification.Code}";

        var template = new EmailVerificationModel(
            verificationLink,
            user.UserName,
            verification.Code
        );

        string emailHtml = await _template.GenerateEmailAsync("Auth/EmailVerification.cshtml", template);

        await _email.SendEmail("Email verification", emailHtml, user.Email.Value);

        return verification;
    }

    public async Task<PasswordVerification> SendPasswordVerification(User user)
    {
        var verification = PasswordVerification.Create();

        // Generate password reset link
        var token = _jwtTokenService.GeneratePasswordResetToken(user);

        var verificationLink = $"{_config["App:FrontendUrl"]}/auth/reset-password/{token}";

        var template = new ResetPasswordModel(
            verificationLink,
            user.UserName
        );

        string emailHtml = await _template.GenerateEmailAsync("Auth/PasswordReset.cshtml", template);

        await _email.SendEmail("Reset Password", emailHtml, user.Email.Value);

        return verification;
    }
}
