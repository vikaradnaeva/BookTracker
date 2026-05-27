using BookTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookTracker.Infrastructure.EntityFramework.Configurations;

public class BookRatingConfiguration : IEntityTypeConfiguration<BookRating>
{
    public void Configure(EntityTypeBuilder<BookRating> builder)
    {
        builder.ToTable("BookRatings");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.BookId).IsRequired();
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.Property(r => r.UpdatedAt);
        builder.HasIndex(r => new { r.UserId, r.BookId }).IsUnique();
    }
}
