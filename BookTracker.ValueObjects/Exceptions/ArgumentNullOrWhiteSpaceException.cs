namespace BookTracker.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException : Exception
{
    public string ParamName { get; }

    public ArgumentNullOrWhiteSpaceException(string paramName)
        : base($"'{paramName}' must not be null, empty, or whitespace.")
    {
        ParamName = paramName;
    }
}
