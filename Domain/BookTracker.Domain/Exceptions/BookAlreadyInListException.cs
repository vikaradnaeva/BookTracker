namespace BookTracker.Domain.Exceptions;

public class BookAlreadyInListException(Guid userId, Guid bookId, string listName)
    : InvalidOperationException($"Book {bookId} is already in the '{listName}' list of user {userId}.");
