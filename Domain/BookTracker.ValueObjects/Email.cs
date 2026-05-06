using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private static readonly EmailValidator _validator = new();

    public Email(string value)
    {
        _validator.Validate(value);
        Value = value.Trim().ToLowerInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Email e) => e.Value;
}
