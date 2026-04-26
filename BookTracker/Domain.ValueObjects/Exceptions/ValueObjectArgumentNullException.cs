
namespace Domain.ValueObjects.Exceptions;
public class ValueObjectArgumentNullException : ArgumentNullException
{
    public ValueObjectArgumentNullException(string paramName, string message)
        : base(paramName, message) {}
}