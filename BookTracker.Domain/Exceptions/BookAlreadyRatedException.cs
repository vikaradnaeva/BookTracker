namespace BookTracker.Domain.Exceptions;

public class BookAlreadyRatedException : DomainException
{
    public Guid UserId { get; }
    public Guid BookId { get; }

    public BookAlreadyRatedException(Guid userId, Guid bookId)
        : base($"User '{userId}' has already rated book '{bookId}'.")
    {
        UserId = userId;
        BookId = bookId;
    }
}
