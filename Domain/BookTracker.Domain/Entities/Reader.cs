using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class Reader : Entity<Guid>
{
    public AuthorName Username { get; private set; }

    private readonly IDictionary<Guid, ReadingStatus> _bookList = new Dictionary<Guid, ReadingStatus>();
    private readonly IDictionary<Guid, ReadingProgress> _readingProgress = new Dictionary<Guid, ReadingProgress>();
    private readonly IDictionary<Guid, BookRating> _ratings = new Dictionary<Guid, BookRating>();

    public IReadOnlyDictionary<Guid, ReadingStatus> BookList => _bookList.AsReadOnly();

    public IReadOnlyDictionary<Guid, ReadingProgress> ReadingProgress => _readingProgress.AsReadOnly();

    public IReadOnlyDictionary<Guid, BookRating> Ratings => _ratings.AsReadOnly();

    public Reader(Guid id, AuthorName username) : base(id)
    {
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
    }

    protected Reader() { }

    public BookRating RateBook(Book book, int rating)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));

        if (_ratings.TryGetValue(book.Id, out var existing))
        {
            existing.SetRating(rating);
            return existing;
        }

        var newRating = new BookRating(Guid.NewGuid(), Id, book.Id, rating, DateTime.UtcNow);
        _ratings[book.Id] = newRating;
        return newRating;
    }

    public ReadingProgress StartReading(Book book)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));

        if (_readingProgress.TryGetValue(book.Id, out var progress))
            return progress; 

        var newProgress = new ReadingProgress(Guid.NewGuid(), Id, book, DateTime.UtcNow);
        _readingProgress[book.Id] = newProgress;
        return newProgress;
    }

    public void UpdateReadingChapter(Book book, int chapter)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        if (!_readingProgress.TryGetValue(book.Id, out var progress))
            throw new BookNotFoundException(book.Id);
        progress.UpdateChapter(chapter);
    }

    public void AddToList(Book book, ReadingStatus status)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        _bookList[book.Id] = status;
    }

    public bool RemoveFromList(Book book)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        return _bookList.Remove(book.Id);
    }
}
