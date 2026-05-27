using BookTracker.Domain.Entities;
using BookTracker.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class ModeratorConfiguration : IEntityTypeConfiguration<Moderator>
{
    public void Configure(EntityTypeBuilder<Moderator> builder)
    {
        builder.ToTable("Moderators");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Username)
            .HasConversion(v => v.Value, v => new AuthorName(v))
            .HasMaxLength(100)
            .IsRequired();
    }
}
