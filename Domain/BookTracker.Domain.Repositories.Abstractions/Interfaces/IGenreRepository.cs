using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IGenreRepository : IRepository<Genre, Guid>
{
    Task<Genre?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
