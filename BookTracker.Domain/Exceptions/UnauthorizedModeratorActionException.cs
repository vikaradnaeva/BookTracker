namespace BookTracker.Domain.Exceptions;

public class UnauthorizedModeratorActionException : DomainException
{
    public Guid UserId { get; }

    public UnauthorizedModeratorActionException(Guid userId)
        : base($"User '{userId}' does not have moderator rights to perform this action.")
    {
        UserId = userId;
    }
}
