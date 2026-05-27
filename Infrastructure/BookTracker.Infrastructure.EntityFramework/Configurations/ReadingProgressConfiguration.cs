using BookTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
{
    public void Configure(EntityTypeBuilder<ReadingProgress> builder)
    {
        builder.ToTable("ReadingProgresses");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.CurrentChapter).IsRequired();
        builder.Property(p => p.StartedAt).IsRequired();
        builder.Property(p => p.LastReadAt).IsRequired();
        builder.HasOne(p => p.Book)
            .WithMany()
            .HasForeignKey("BookId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
