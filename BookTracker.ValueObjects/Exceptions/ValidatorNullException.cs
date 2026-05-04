namespace BookTracker.ValueObjects.Exceptions;
public class ValidatorNullException : Exception
{
    public ValidatorNullException(string valueObjectName)
        : base($"Validator for '{valueObjectName}' must not be null.") { }
}
