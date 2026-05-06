using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class Book : Entity
{
    public BookTitle Title { get; private set; }
    public Author Author { get; private set; }
    public Description Description { get; private set; }
    public int TotalChapters { get; private set; }
    public double AverageRating { get; private set; }
    public int RatingsCount { get; private set; }
    public bool IsDeleted { get; private set; }

    private readonly List<Genre> _genres = new();
    private readonly List<Tag> _tags = new();

    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();
    public IReadOnlyList<Tag> Tags => _tags.AsReadOnly();

    public Book(BookTitle title, Author author, Description description, int totalChapters) : base()
    {
        if (totalChapters < 1)
            throw new InvalidBookDataException("Book must have at least one chapter.");

        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        TotalChapters = totalChapters;
        AverageRating = 0;
        RatingsCount = 0;
        IsDeleted = false;
    }

    public void UpdateTagsAndGenres(IEnumerable<Genre> genres, IEnumerable<Tag> tags)
    {
        if (IsDeleted)
            throw new BookDeletedException(Id);

        _genres.Clear();
        _genres.AddRange(genres.Where(g => g is not null).Distinct());

        _tags.Clear();
        _tags.AddRange(tags.Where(t => t is not null).Distinct());
    }

    public void AddRating(double rating)
    {
        if (IsDeleted)
            throw new BookDeletedException(Id);

        if (rating < 1.0 || rating > 5.0)
            throw new InvalidRatingException(rating);

        AverageRating = (AverageRating * RatingsCount + rating) / (RatingsCount + 1);
        RatingsCount++;
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new BookAlreadyDeletedException(Id);

        IsDeleted = true;
    }
}
