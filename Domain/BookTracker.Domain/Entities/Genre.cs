using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class Genre : Entity<Guid>
{
    public GenreName Name { get; private set; }

    public Genre(Guid id, GenreName name) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
    }

    protected Genre() { }

    internal void Rename(GenreName newName)
    {
        Name = newName ?? throw new ArgumentNullValueException(nameof(newName));
    }
}
