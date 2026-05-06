namespace BookTracker.Domain.Exceptions;

public class BookDeletedException : DomainException
{
    public Guid BookId { get; }

    public BookDeletedException(Guid bookId)
        : base($"Book with ID '{bookId}' has been deleted and cannot be accessed.")
    {
        BookId = bookId;
    }
}
