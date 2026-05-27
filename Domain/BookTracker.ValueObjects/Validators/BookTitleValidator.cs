using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Exceptions;

namespace BookTracker.ValueObjects.Validators;

public class BookTitleValidator : IValidator<string>
{
    public static int MAX_LENGTH => 200;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}
