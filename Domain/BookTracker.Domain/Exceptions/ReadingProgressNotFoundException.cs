namespace BookTracker.Domain.Exceptions;

public class ReadingProgressNotFoundException : DomainException
{
    public Guid UserId { get; }
    public Guid BookId { get; }

    public ReadingProgressNotFoundException(Guid userId, Guid bookId)
        : base($"No reading progress found for user '{userId}' and book '{bookId}'. Call StartReading first.")
    {
        UserId = userId;
        BookId = bookId;
    }
}
