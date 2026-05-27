using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IReadingProgressRepository : IRepository<ReadingProgress, Guid>
{
    Task<ReadingProgress?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken);
}
