using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IReaderRepository : IRepository<Reader, Guid>
{
    Task<Reader?> GetWithDetailsAsync(Guid readerId, CancellationToken cancellationToken);
}
