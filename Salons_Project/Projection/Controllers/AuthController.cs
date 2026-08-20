using MediatR;
using Microsoft.AspNetCore.Http;
using Salons_Project.Application.Features.Auth.ResetPassword;
using Salons_Project.Application.Features.Auth.VerifyEmail;
using Salons_Project.Application.Features.Auth.Register;
using Salons_Project.Application.Features.Auth.Login;
using Microsoft.AspNetCore.Mvc;
using Salons_Project.Application.Features.Auth.ResendVerificationCode;

namespace Salons_Project.Api.Projection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest req, CancellationToken ct)
    {
        var cmd = new RegisterCommand(req.UserName, req.Email, req.Password, req.Age , req.PhoneNumber);

        var result = await _mediator.Send(cmd, ct);


        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req, CancellationToken ct)
    {
        var cmd = new LoginCommand(req.Email, req.Password);
        var result = await _mediator.Send(cmd);

        return Ok(result);
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail(VerifyEmailRequest req, CancellationToken ct)
    {
        var cmd = new VerifyEmailCommand(req.Email, req.Code);

        var result = await _mediator.Send(cmd, ct);

        return Ok(result);
    }

    [HttpPost("resend-code")]
    public async Task<IActionResult> ResendVerification(ResendVerificationRequest req, CancellationToken ct)
    {
        var cmd = new ResendVerificationCommand(req.Email);

        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }


    [HttpPost("send-password-reset")]
    public async Task<IActionResult> SendPasswordReset(ResetPasswordRequest req, CancellationToken ct)
    {
        var cmd = new ResetPasswordCommand(req.Email);
        var result = await _mediator.Send(cmd, ct);
        return Ok(result);
    }

}
