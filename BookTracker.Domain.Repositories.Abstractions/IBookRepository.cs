using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IBookRepository : IRepository<Book>
{
    Task<IReadOnlyList<Book>> GetByGenreAsync(string genre, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> GetByTagAsync(string tag, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> GetByAuthorAsync(string author, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> SearchByTitleAsync(string titlePart, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> GetAllActiveAsync(CancellationToken cancellationToken = default);
}
