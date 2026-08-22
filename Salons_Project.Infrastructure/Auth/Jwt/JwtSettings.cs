
namespace Salons_Project.Infrastructure.Auth.Jwt;

public class JwtSettings
{
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
    public string Key { get; init; } = "";
    public int AccessTokenMinutes { get; init; } = 1200;
}
