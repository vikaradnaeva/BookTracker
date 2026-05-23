using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class Tag : Entity<Guid>
{
    public TagName Name { get; private set; }

    public Tag(Guid id, TagName name) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
    }

    protected Tag() { }

    internal void Rename(TagName newName)
    {
        Name = newName ?? throw new ArgumentNullValueException(nameof(newName));
    }
}
