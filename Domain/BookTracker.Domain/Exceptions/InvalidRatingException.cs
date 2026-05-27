namespace BookTracker.Domain.Exceptions;

public class InvalidRatingException(int rating)
    : ArgumentException($"Rating {rating} is invalid. Rating must be between 1 and 5.");
