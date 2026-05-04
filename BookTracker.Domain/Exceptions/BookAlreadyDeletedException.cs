namespace BookTracker.Domain.Exceptions;

public class BookAlreadyDeletedException : DomainException
{
    public Guid BookId { get; }

    public BookAlreadyDeletedException(Guid bookId)
        : base($"Book with ID '{bookId}' is already deleted.")
    {
        BookId = bookId;
    }
}
