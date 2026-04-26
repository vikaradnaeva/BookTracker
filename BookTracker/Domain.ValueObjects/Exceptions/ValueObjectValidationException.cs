namespace Domain.ValueObjects.Base.Exceptions;

public class ValueObjectValidationException : Exception
{
    public ValueObjectValidationException(string message) : base(message)
    {
    }

    public ValueObjectValidationException(string message, Exception innerException)
        : base(message, innerException) {}
}