using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;

namespace BookTracker.Domain.Entities;

public class BookRating : Entity
{
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public double Value { get; private set; }
    public DateTime RatedAt { get; private set; }

    public BookRating(Guid userId, Guid bookId, double value) : base()
    {
        if (value < 1.0 || value > 5.0)
            throw new InvalidRatingException(value);

        UserId = userId;
        BookId = bookId;
        Value = value;
        RatedAt = DateTime.UtcNow;
    }
}
