using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class Genre : ValueObject
{
    public string Value { get; }

    private static readonly GenreValidator _validator = new();

    public Genre(string value)
    {
        _validator.Validate(value);
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;

    public static implicit operator string(Genre g) => g.Value;
}
