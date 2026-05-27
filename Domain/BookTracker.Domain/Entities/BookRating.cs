using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;

namespace BookTracker.Domain.Entities;

public class BookRating : Entity<Guid>
{
    public const int MIN_RATING = 1;
    public const int MAX_RATING = 5;

    public Guid UserId { get; }
    public Guid BookId { get; }
    public int Rating { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    public BookRating(Guid id, Guid userId, Guid bookId, int rating, DateTime createdAt) : base(id)
    {
        UserId = userId;
        BookId = bookId;
        CreatedAt = createdAt;
        SetRating(rating);
    }

    protected BookRating() { }

    public void SetRating(int rating)
    {
        if (rating < MIN_RATING || rating > MAX_RATING)
            throw new InvalidRatingException(rating);
        Rating = rating;
        UpdatedAt = DateTime.UtcNow;
    }
}
