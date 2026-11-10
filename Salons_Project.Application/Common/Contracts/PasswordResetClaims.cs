namespace Salons_Project.Application.Common.Contracts;

public sealed record PasswordResetClaims(int UserId, string Email, string Code);
