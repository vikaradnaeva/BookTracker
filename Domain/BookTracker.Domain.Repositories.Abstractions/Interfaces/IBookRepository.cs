using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IBookRepository : IRepository<Book, Guid>
{
    Task<IEnumerable<Book>> FindByTitleAsync(string titleQuery, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetByGenreAsync(Guid genreId, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetByTagAsync(Guid tagId, CancellationToken cancellationToken);
}
