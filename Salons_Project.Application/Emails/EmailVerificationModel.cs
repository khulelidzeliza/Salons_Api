
namespace Salons_Project.Application.Emails;
public sealed record EmailVerificationModel(
    string VerificationLink,
    string Username,
    string Code
);

public sealed record ResetPasswordModel(
    string ResetLink,
    string UserName
);