using BookTracker.Domain.Entities;
using BookTracker.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name)
            .HasConversion(v => v.Value, v => new GenreName(v))
            .HasMaxLength(50)
            .IsRequired();
    }
}
