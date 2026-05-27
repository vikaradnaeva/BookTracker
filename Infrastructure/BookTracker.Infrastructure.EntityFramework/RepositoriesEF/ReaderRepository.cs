using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class ReaderRepository(BookTrackerDbContext context) : IReaderRepository
{
    public async Task<IEnumerable<Reader>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.Readers.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Reader?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Readers.FindAsync([id], cancellationToken);

    public async Task<Reader?> AddAsync(Reader entity, CancellationToken cancellationToken)
    {
        var entry = await context.Readers.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(Reader entity, CancellationToken cancellationToken)
    {
        context.Readers.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Reader entity, CancellationToken cancellationToken)
    {
        context.Readers.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }

    public async Task<Reader?> GetWithDetailsAsync(Guid readerId, CancellationToken cancellationToken)
        => await context.Readers
            .FirstOrDefaultAsync(r => r.Id == readerId, cancellationToken);
}
