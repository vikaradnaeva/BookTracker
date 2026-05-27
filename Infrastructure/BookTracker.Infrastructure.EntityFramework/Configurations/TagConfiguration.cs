using BookTracker.Domain.Entities;
using BookTracker.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name)
            .HasConversion(v => v.Value, v => new TagName(v))
            .HasMaxLength(30)
            .IsRequired();
    }
}
