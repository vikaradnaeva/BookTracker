using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IReadingProgressRepository : IRepository<ReadingProgress>
{
    Task<ReadingProgress?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ReadingProgress>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
