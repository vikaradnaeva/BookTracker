using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class Username : ValueObject
{
    public string Value { get; }

    private static readonly UsernameValidator _validator = new();

    public Username(string value)
    {
        _validator.Validate(value);
        Value = value.Trim().ToLowerInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Username u) => u.Value;
}
