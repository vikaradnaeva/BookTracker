using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class BookTitle : ValueObject
{
    public string Value { get; }

    private static readonly BookTitleValidator _validator = new();

    public BookTitle(string value)
    {
        _validator.Validate(value);
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;

    public static implicit operator string(BookTitle title) => title.Value;
}
