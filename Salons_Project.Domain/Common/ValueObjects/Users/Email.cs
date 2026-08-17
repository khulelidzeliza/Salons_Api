using Salons_Project.Domain.Common.Results;
using Salons_Project.Domain.Common.Guards;

namespace Salons_Project.Domain.Common.ValueObjects.Users;

public sealed class Email : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private Email() { }
    private Email(string value) => Value = value;

    public static Result<Email> Create(string value)
    {
        var nullCheck = Guard.AgainstNullOrEmpty(value, Errors.Invalid("Email is required!"));

        if (!nullCheck.IsSuccess) return Result<Email>.Failure(nullCheck.Error!);

        var formatCheck =
            Guard.AgainstRegex(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", Errors.Invalid("Invalid email format!"));

        if (!formatCheck.IsSuccess) return Result<Email>.Failure(formatCheck.Error!);

        return Result<Email>.Success(new Email(value));
    }

    //protected override IEnumerable<object?> GetEqualityComponents()
    //{
    //    yield return Value.ToLowerInvariant();
    //}
}
