using BookTracker.Domain.Entities;
using BookTracker.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.Infrastructure.EntityFramework;

public class BookTrackerDbContext(DbContextOptions<BookTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Reader> Readers => Set<Reader>();
    public DbSet<Moderator> Moderators => Set<Moderator>();
    public DbSet<BookRating> BookRatings => Set<BookRating>();
    public DbSet<ReadingProgress> ReadingProgresses => Set<ReadingProgress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new ReaderConfiguration());
        modelBuilder.ApplyConfiguration(new ModeratorConfiguration());
        modelBuilder.ApplyConfiguration(new BookRatingConfiguration());
        modelBuilder.ApplyConfiguration(new ReadingProgressConfiguration());
    }
}
