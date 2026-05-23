namespace BookTracker.Domain.Exceptions;

public class BookNotFoundException(Guid bookId)
    : InvalidOperationException($"Book with id {bookId} was not found.");
