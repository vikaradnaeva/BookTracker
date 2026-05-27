using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class ReadingProgressRepository(BookTrackerDbContext context) : IReadingProgressRepository
{
    public async Task<IEnumerable<ReadingProgress>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.ReadingProgresses.Include(p => p.Book).AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<ReadingProgress?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.ReadingProgresses
            .Include(p => p.Book)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<ReadingProgress?> AddAsync(ReadingProgress entity, CancellationToken cancellationToken)
    {
        var entry = await context.ReadingProgresses.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(ReadingProgress entity, CancellationToken cancellationToken)
    {
        context.ReadingProgresses.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(ReadingProgress entity, CancellationToken cancellationToken)
    {
        context.ReadingProgresses.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }

    public async Task<ReadingProgress?> GetByUserAndBookAsync(Guid userId, Guid bookId, CancellationToken cancellationToken)
        => await context.ReadingProgresses
            .Include(p => p.Book)
            .FirstOrDefaultAsync(p => p.UserId == userId
                && EF.Property<Guid>(p, "BookId") == bookId, cancellationToken);
}
