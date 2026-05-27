using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions.Base;

namespace BookTracker.Domain.Repositories.Abstractions;

public interface IBookRatingRepository : IRepository<BookRating, Guid>
{
    Task<BookRating?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken);
    Task<double> GetAverageRatingAsync(Guid bookId, CancellationToken cancellationToken);
}
