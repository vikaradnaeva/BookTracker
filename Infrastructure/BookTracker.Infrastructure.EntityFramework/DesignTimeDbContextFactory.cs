using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookTracker.Infrastructure.EntityFramework;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BookTrackerDbContext>
{
    public BookTrackerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookTrackerDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5433;Database=BookTracker;Username=postgres;Password=1234");

        return new BookTrackerDbContext(optionsBuilder.Options);
    }
}
