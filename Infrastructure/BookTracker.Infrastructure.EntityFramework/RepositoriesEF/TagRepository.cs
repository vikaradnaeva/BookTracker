using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class TagRepository(BookTrackerDbContext context) : ITagRepository
{
    public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.Tags.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Tags.FindAsync([id], cancellationToken);

    public async Task<Tag?> AddAsync(Tag entity, CancellationToken cancellationToken)
    {
        var entry = await context.Tags.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(Tag entity, CancellationToken cancellationToken)
    {
        context.Tags.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Tag entity, CancellationToken cancellationToken)
    {
        context.Tags.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }

    public async Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => await context.Tags
            .FirstOrDefaultAsync(t => EF.Property<string>(t, "Name") == name, cancellationToken);
}
