using Salons_Project.Domain.Common;
using Salons_Project.Domain.Common.Guards;
using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Common.ValueObjects.Users;
using Salons_Project.Domain.Enums.User;
using Salons_Project.Domain.Models.Auth;
using Salons_Project.Domain.Models.Content;
using Salons_Project.Domain.Models.Organization;

namespace Salons_Project.Domain.Models.Users;

public class User : BaseEntity
{
    public int Age { get; set; }
    public Email Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsVerified { get; set; } = false;
    public Roles Role { get; set; } = Roles.User;
    public Genders Gender { get; set; } = Genders.Male;

    public List<Salon> Salons { get; set; } = new();
    public List<Review> Reviews { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
    public EmailVerification EmailVerification { get; private set; }
    public PasswordVerification PasswordEmailVerification { get; private set; }
    private User() { }

    public User(string username, Email email)
    {
        UserName = username;
        Email = email;
    }
    public static Result<User> Create(string username, string email)
    {
        var firstNameResult = Guard.AgainstStringRange(username, 2, 40,
            Errors.Invalid("Username must be between 4 and 60 characters"));

        if (!firstNameResult.IsSuccess) return Result<User>.Failure(firstNameResult.Error!);

        var emailResult = Email.Create(email);

        if (!emailResult.IsSuccess) return Result<User>.Failure(emailResult.Error!);

        return Result<User>.Success(new User(username, emailResult.Value!));
    }
    public void SetPassword(string password) => Password = password;

    public void Rename(string? username)
    {
        if (!string.IsNullOrWhiteSpace(username))
            UserName = username;
    }

    public void Verify() => IsVerified = true;

    public void SetEmailVerification(EmailVerification verification)
        => EmailVerification = verification;

    public void SetPasswordEmailVerification(PasswordVerification verification)
        => PasswordEmailVerification = verification;

}
