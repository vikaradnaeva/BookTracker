namespace BookTracker.Domain.Exceptions;

public class InvalidRatingException : DomainException
{
    public double AttemptedRating { get; }

    public InvalidRatingException(double rating)
        : base($"Rating value '{rating}' is invalid. Must be between 1.0 and 5.0.")
    {
        AttemptedRating = rating;
    }
}
