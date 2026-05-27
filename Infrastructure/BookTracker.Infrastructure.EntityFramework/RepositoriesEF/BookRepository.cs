using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework.Repositories;

public class BookRepository(BookTrackerDbContext context) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        var query = context.Books.Include(b => b.Genres).Include(b => b.Tags).AsQueryable();
        if (asNoTracking) query = query.AsNoTracking();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Books
            .Include(b => b.Genres)
            .Include(b => b.Tags)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<Book?> AddAsync(Book entity, CancellationToken cancellationToken)
    {
        var entry = await context.Books.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }

    public async Task<bool> UpdateAsync(Book entity, CancellationToken cancellationToken)
    {
        context.Books.Update(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Book entity, CancellationToken cancellationToken)
    {
        context.Books.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await DeleteAsync(entity, cancellationToken);
    }

    public async Task<IEnumerable<Book>> FindByTitleAsync(string titleQuery, CancellationToken cancellationToken)
        => await context.Books
            .Include(b => b.Genres)
            .Include(b => b.Tags)
            .Where(b => EF.Property<string>(b, "Title").Contains(titleQuery))
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Book>> GetByGenreAsync(Guid genreId, CancellationToken cancellationToken)
        => await context.Books
            .Include(b => b.Genres)
            .Include(b => b.Tags)
            .Where(b => b.Genres.Any(g => g.Id == genreId))
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Book>> GetByTagAsync(Guid tagId, CancellationToken cancellationToken)
        => await context.Books
            .Include(b => b.Genres)
            .Include(b => b.Tags)
            .Where(b => b.Tags.Any(t => t.Id == tagId))
            .ToListAsync(cancellationToken);
}
