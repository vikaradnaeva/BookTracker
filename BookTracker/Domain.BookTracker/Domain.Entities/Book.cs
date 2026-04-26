using Domain.BookTracker.Domain.Entities.Shared;
using Domain.ValueObjects;

namespace Domain.BookTracker.Domain.Entities;

public class Book : BaseEntity
{
    public BookTitle Title { get; set; }
    public AuthorName Author { get; set; }
    public string Description { get; set; } = string.Empty;
    public int TotalChapters { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }

    public Book(string title, string author)
    {
        Title = BookTitle.Create(title);
        Author = AuthorName.Create(author);
    }

    public void AddRating(int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Оценка может быть от 1 до 5.");

        var totalScore = AverageRating * RatingCount + rating;
        RatingCount++;
        AverageRating = totalScore / RatingCount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddGenre(Genre genre)
    {
        if (!Genres.Any(g => g.Name == genre.Name))
        {
            Genres.Add(genre);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void AddTag(Tag tag)
    {
        if (!Tags.Any(t => t.Name == tag.Name))
        {
            Tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveGenre(Genre genre)
    {
        Genres.RemoveAll(g => g.Name == genre.Name);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveTag(Tag tag)
    {
        Tags.RemoveAll(t => t.Name == tag.Name);
        UpdatedAt = DateTime.UtcNow;
    }
}