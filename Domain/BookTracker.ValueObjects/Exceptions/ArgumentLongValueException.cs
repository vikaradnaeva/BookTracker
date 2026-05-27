namespace BookTracker.ValueObjects.Exceptions;

public class ArgumentLongValueException(string paramName, string value, int maxLength)
    : FormatException($"The \"{paramName}\" length {value.Length} exceeds maximum allowed length {maxLength}.")
{
    public string Value => value;
    public int MaxLength => maxLength;
}
