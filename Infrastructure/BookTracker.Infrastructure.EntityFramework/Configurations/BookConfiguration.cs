using BookTracker.Domain.Entities;
using BookTracker.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .HasConversion(v => v.Value, v => new BookTitle(v))
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Author)
            .HasConversion(v => v.Value, v => new AuthorName(v))
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.TotalChapters).IsRequired();

        builder.HasMany(b => b.Genres)
            .WithMany()
            .UsingEntity(j => j.ToTable("BookGenres"));

        builder.HasMany(b => b.Tags)
            .WithMany()
            .UsingEntity(j => j.ToTable("BookTags"));
    }
}
