using Domain.BookTracker.Domain.Entities.Shared;
using Domain.ValueObjects;

namespace Domain.BookTracker.Domain.Entities;

public class Genre : BaseEntity
{
    public GenreName Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public Genre(string name)
    {
        Name = GenreName.Create(name);
    }

    public Genre(string name, string description) : this(name)
    {
        Description = description;
    }
}