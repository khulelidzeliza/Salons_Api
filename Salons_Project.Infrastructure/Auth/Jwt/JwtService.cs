using Microsoft.IdentityModel.Tokens;
using Salons_Project.Application.Common.Interfaces;
using Salons_Project.Domain.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Salons_Project.Application.Common.Contracts;

namespace Salons_Project.Infrastructure.Auth.Jwt;

public sealed class JwtService : IJwtService
{
    private readonly JwtSettings _s;

    public JwtService(JwtSettings s) => _s = s;

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new(JwtRegisteredClaimNames.Email, user.Email.Value),
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Role, user.Role.ToString()),
    };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_s.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _s.Issuer,
            audience: _s.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_s.AccessTokenMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public string GeneratePasswordResetToken(User user)
    {
        var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new(JwtRegisteredClaimNames.Email, user.Email.Value),
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Email, user.Email.Value),
        new("pwd_reset", "true"),
        new("CODE", user.PasswordEmailVerification?.Code ?? string.Empty),
    };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_s.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _s.Issuer,
            audience: _s.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public PasswordResetClaims? ReadPasswordResetToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _s.Issuer,
            ValidateAudience = true,
            ValidAudience = _s.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_s.Key)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
        };

        try
        {
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out _);

            if (principal.FindFirst("pwd_reset")?.Value != "true") return null;

            var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (!int.TryParse(id, out var userId)) return null;

            var email = principal.FindFirst(ClaimTypes.Email)?.Value
                        ?? principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value
                        ?? string.Empty;
            var code = principal.FindFirst("CODE")?.Value ?? string.Empty;

            return new PasswordResetClaims(userId, email, code);
        }
        catch (SecurityTokenException) { return null; }
        catch (ArgumentException) { return null; }
    }
}
