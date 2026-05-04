namespace BookTracker.Domain.Exceptions;

public class InvalidBookDataException : DomainException
{
    public InvalidBookDataException(string message) : base(message) { }
}
