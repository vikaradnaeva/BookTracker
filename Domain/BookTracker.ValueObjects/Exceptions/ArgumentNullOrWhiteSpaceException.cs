namespace BookTracker.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName)
    : ArgumentNullException(paramName, $"The \"{paramName}\" must not be null, empty or whitespace.");
