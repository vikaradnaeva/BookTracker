using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class User : Entity
{
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public UserRole Role { get; private set; }

    private readonly List<ReadingProgress> _readingProgresses = new();
    private readonly List<BookListEntry> _bookListEntries = new();
    private readonly List<BookRating> _ratings = new();

    public IReadOnlyList<ReadingProgress> ReadingProgresses => _readingProgresses.AsReadOnly();
    public IReadOnlyList<BookListEntry> BookListEntries => _bookListEntries.AsReadOnly();
    public IReadOnlyList<BookRating> Ratings => _ratings.AsReadOnly();

    public User(Username username, Email email, UserRole role = UserRole.Reader) : base()
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Role = role;
    }

    public ReadingProgress StartReading(Book book)
    {
        if (book.IsDeleted)
            throw new BookDeletedException(book.Id);

        var existing = _readingProgresses.FirstOrDefault(p => p.BookId == book.Id);
        if (existing is not null)
            return existing;

        var progress = new ReadingProgress(Id, book.Id, book.TotalChapters);
        _readingProgresses.Add(progress);
        return progress;
    }

    public void UpdateReadingProgress(Guid bookId, int currentChapter)
    {
        var progress = _readingProgresses.FirstOrDefault(p => p.BookId == bookId)
            ?? throw new ReadingProgressNotFoundException(Id, bookId);

        progress.UpdateChapter(currentChapter);
    }

    public void AddBookToList(Book book, BookListType listType)
    {
        if (book.IsDeleted)
            throw new BookDeletedException(book.Id);

        var existing = _bookListEntries.FirstOrDefault(e => e.BookId == book.Id);
        if (existing is not null)
        {
            existing.ChangeListType(listType);
            return;
        }

        _bookListEntries.Add(new BookListEntry(Id, book.Id, listType));
    }

    public void RateBook(Book book, double rating)
    {
        if (book.IsDeleted)
            throw new BookDeletedException(book.Id);

        if (_ratings.Any(r => r.BookId == book.Id))
            throw new BookAlreadyRatedException(Id, book.Id);

        _ratings.Add(new BookRating(Id, book.Id, rating));
        book.AddRating(rating);
    }
    public bool IsModerator() => Role == UserRole.Moderator;
}
