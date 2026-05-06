using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Exceptions;

namespace BookTracker.ValueObjects.Validators;

public class EmailValidator : IValidator<string>
{
    public const int MaxLength = 254;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (!value.Contains('@') || !value.Contains('.'))
            throw new ArgumentNullOrWhiteSpaceException("email format");
        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), MaxLength);
    }
}
