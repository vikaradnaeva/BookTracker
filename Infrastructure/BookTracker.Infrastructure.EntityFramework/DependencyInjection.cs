using BookTracker.Domain.Repositories.Abstractions;
using BookTracker.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookTracker.Infrastructure.EntityFramework;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BookTrackerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IReaderRepository, ReaderRepository>();
        services.AddScoped<IModeratorRepository, ModeratorRepository>();
        services.AddScoped<IBookRatingRepository, BookRatingRepository>();
        services.AddScoped<IReadingProgressRepository, ReadingProgressRepository>();

        return services;
    }
}
