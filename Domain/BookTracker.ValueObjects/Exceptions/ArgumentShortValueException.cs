namespace BookTracker.ValueObjects.Exceptions;

public class ArgumentShortValueException : Exception
{
    public string ParamName { get; }
    public int MinLength { get; }

    public ArgumentShortValueException(string paramName, int minLength)
        : base($"'{paramName}' must be at least {minLength} character(s) long.")
    {
        ParamName = paramName;
        MinLength = minLength;
    }
}
