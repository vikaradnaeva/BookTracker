using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class GenreRepository(BookTrackerDbContext context) : IGenreRepository
{
    public async Task<IEnumerable<Genre>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.Genres.AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Genres.FindAsync([id], cancellationToken);

    public async Task<Genre?> AddAsync(Genre entity, CancellationToken cancellationToken)
    {
        var entry = await context.Genres.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(Genre entity, CancellationToken cancellationToken)
    {
        context.Genres.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Genre entity, CancellationToken cancellationToken)
    {
        context.Genres.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }

    public async Task<Genre?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => await context.Genres
            .FirstOrDefaultAsync(g => EF.Property<string>(g, "Name") == name, cancellationToken);
}
