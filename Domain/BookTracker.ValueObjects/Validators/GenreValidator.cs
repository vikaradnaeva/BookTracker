using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Exceptions;

namespace BookTracker.ValueObjects.Validators;

public class GenreValidator : IValidator<string>
{
    public const int MinLength = 1;
    public const int MaxLength = 50;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length < MinLength)
            throw new ArgumentShortValueException(nameof(value), MinLength);
        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), MaxLength);
    }
}
