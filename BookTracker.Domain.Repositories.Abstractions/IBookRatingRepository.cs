using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IBookRatingRepository : IRepository<BookRating>
{
    Task<BookRating?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookRating>> GetAllByBookAsync(Guid bookId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookRating>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
