using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Validators;

namespace BookTracker.ValueObjects;

public class Author : ValueObject
{
    public string Value { get; }

    private static readonly AuthorValidator _validator = new();

    public Author(string value)
    {
        _validator.Validate(value);
        Value = value.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;

    public static implicit operator string(Author author) => author.Value;
}
