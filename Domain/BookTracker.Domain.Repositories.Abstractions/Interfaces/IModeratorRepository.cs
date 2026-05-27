using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IModeratorRepository : IRepository<Moderator, Guid>
{
}
