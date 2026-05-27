using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class BookRatingRepository(BookTrackerDbContext context) : IBookRatingRepository
{
    public async Task<IEnumerable<BookRating>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.BookRatings.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<BookRating?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.BookRatings.FindAsync([id], cancellationToken);

    public async Task<BookRating?> AddAsync(BookRating entity, CancellationToken cancellationToken)
    {
        var entry = await context.BookRatings.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(BookRating entity, CancellationToken cancellationToken)
    {
        context.BookRatings.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(BookRating entity, CancellationToken cancellationToken)
    {
        context.BookRatings.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }

    public async Task<BookRating?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken)
        => await context.BookRatings
            .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId, cancellationToken);

    public async Task<double> GetAverageRatingAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var ratings = await context.BookRatings
            .Where(r => r.BookId == bookId)
            .Select(r => r.Rating)
            .ToListAsync(cancellationToken);
        return ratings.Count == 0 ? 0.0 : ratings.Average();
    }
}
