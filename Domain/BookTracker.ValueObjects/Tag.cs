using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class Tag : ValueObject
{
    public string Value { get; }

    private static readonly TagValidator _validator = new();

    public Tag(string value)
    {
        _validator.Validate(value);
        Value = value.Trim().ToLowerInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Tag t) => t.Value;
}
