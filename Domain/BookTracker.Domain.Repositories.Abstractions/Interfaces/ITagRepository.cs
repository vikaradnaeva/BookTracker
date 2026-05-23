using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface ITagRepository : IRepository<Tag, Guid>
{
    Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
