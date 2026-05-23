namespace BookTracker.Domain.Exceptions;

public class UserNotModeratorException(Guid userId)
    : InvalidOperationException($"User {userId} does not have moderator permissions.");
