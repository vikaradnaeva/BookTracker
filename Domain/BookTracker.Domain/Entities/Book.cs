using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class Book : Entity<Guid>
{
    public BookTitle Title { get; private set; }
    public AuthorName Author { get; private set; }
    public int TotalChapters { get; private set; }

    private readonly ICollection<Genre> _genres = [];
    private readonly ICollection<Tag> _tags = [];

    public IReadOnlyCollection<Genre> Genres => _genres.ToList().AsReadOnly();
    public IReadOnlyCollection<Tag> Tags => _tags.ToList().AsReadOnly();

    public Book(Guid id, BookTitle title, AuthorName author, int totalChapters) : base(id)
    {
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Author = author ?? throw new ArgumentNullValueException(nameof(author));
        TotalChapters = totalChapters > 0
            ? totalChapters
            : throw new ArgumentException("Total chapters must be greater than 0.");
    }

    protected Book() { }

    internal void SetTitle(BookTitle newTitle)
    {
        Title = newTitle ?? throw new ArgumentNullValueException(nameof(newTitle));
    }

    internal void SetAuthor(AuthorName newAuthor)
    {
        Author = newAuthor ?? throw new ArgumentNullValueException(nameof(newAuthor));
    }

    internal void AddGenre(Genre genre)
    {
        if (genre == null) throw new ArgumentNullValueException(nameof(genre));
        if (!_genres.Contains(genre))
            _genres.Add(genre);
    }

    internal void RemoveGenre(Genre genre)
    {
        if (genre == null) throw new ArgumentNullValueException(nameof(genre));
        _genres.Remove(genre);
    }

    internal void AddTag(Tag tag)
    {
        if (tag == null) throw new ArgumentNullValueException(nameof(tag));
        if (!_tags.Contains(tag))
            _tags.Add(tag);
    }

    internal void RemoveTag(Tag tag)
    {
        if (tag == null) throw new ArgumentNullValueException(nameof(tag));
        _tags.Remove(tag);
    }
}
