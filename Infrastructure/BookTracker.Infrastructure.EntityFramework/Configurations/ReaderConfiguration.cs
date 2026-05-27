using BookTracker.Domain.Entities;
using BookTracker.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
{
    public void Configure(EntityTypeBuilder<Reader> builder)
    {
        builder.ToTable("Readers");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Username)
            .HasConversion(v => v.Value, v => new AuthorName(v))
            .HasMaxLength(100)
            .IsRequired();
    }
}
