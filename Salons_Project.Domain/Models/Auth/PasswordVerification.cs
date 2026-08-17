



using Salons_Project.Domain.Common;
using Salons_Project.Domain.Models.Users;

namespace Salons_Project.Domain.Models.Auth;

public class PasswordVerification : Verification
{

    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public static PasswordVerification Create()
    {
        Random random = new();
        string code = random.Next(100_000, 9999_99).ToString();

        return new PasswordVerification() { Code = code };
    }

    public void SetUserId(int userId) => UserId = userId;
}
