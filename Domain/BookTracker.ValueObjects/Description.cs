using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class Description : ValueObject
{
    public string Value { get; }

    private static readonly DescriptionValidator _validator = new();

    public Description(string value)
    {
        _validator.Validate(value ?? string.Empty);
        Value = value?.Trim() ?? string.Empty;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Description d) => d.Value;
}
