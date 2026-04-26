using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects.Base;

public class ValueObject<T> : IEquatable<ValueObject<T>>
{
    public T Value { get; }

    protected ValueObject(IValidator<T> validator, T value)
    {
        if (validator is null)
            throw new ValueObjectArgumentNullException(nameof(validator), "Validator cannot be null");

        validator.Validate(value);

        Value = value;
    }

    public override string ToString() => Value?.ToString() ?? GetType().ToString();

    public override bool Equals(object? obj) => Equals(obj as ValueObject<T>);

    public bool Equals(ValueObject<T>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode() => Value?.GetHashCode() ?? 0;

    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) => Equals(left, right);

    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) => !(left == right);
}