using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IBookListEntryRepository : IRepository<BookListEntry>
{
    Task<IReadOnlyList<BookListEntry>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookListEntry>> GetByUserAndListTypeAsync(Guid userId, BookListType listType, CancellationToken cancellationToken = default);

    Task<BookListEntry?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken = default);
}
