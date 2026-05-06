namespace BookTracker.ValueObjects.Exceptions;

public class ArgumentLongValueException : Exception
{
    public string ParamName { get; }
    public int MaxLength { get; }

    public ArgumentLongValueException(string paramName, int maxLength)
        : base($"'{paramName}' must not exceed {maxLength} character(s).")
    {
        ParamName = paramName;
        MaxLength = maxLength;
    }
}
