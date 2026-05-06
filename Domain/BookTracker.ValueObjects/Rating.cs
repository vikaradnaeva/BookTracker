using BookTracker.ValueObjects.Base;
using BookTracker.ValueObjects.Exceptions;

namespace BookTracker.ValueObjects;

public class Rating : ValueObject
{
    public const double Min = 1.0;
    public const double Max = 5.0;

    public double Value { get; }

    public Rating(double value)
    {
        if (value < Min || value > Max)
            throw new ArgumentNullOrWhiteSpaceException(
                $"Rating must be between {Min} and {Max}, got {value}.");

        Value = Math.Round(value, 1);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString("F1");

    public static implicit operator double(Rating r) => r.Value;
}
