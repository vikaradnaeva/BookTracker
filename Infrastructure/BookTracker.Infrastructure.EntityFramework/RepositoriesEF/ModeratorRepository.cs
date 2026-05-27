using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class ModeratorRepository(BookTrackerDbContext context) : IModeratorRepository
{
    public async Task<IEnumerable<Moderator>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.Moderators.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Moderator?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Moderators.FindAsync([id], cancellationToken);

    public async Task<Moderator?> AddAsync(Moderator entity, CancellationToken cancellationToken)
    {
        var entry = await context.Moderators.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(Moderator entity, CancellationToken cancellationToken)
    {
        context.Moderators.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Moderator entity, CancellationToken cancellationToken)
    {
        context.Moderators.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }
}
