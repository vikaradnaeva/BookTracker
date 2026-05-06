using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Exceptions;

namespace BookTracker.ValueObjects.Validators;

public class DescriptionValidator : IValidator<string>
{
    public const int MaxLength = 2000;

    public void Validate(string value)
    {
        if (value is null)
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), MaxLength);
    }
}
