using Domain.BookTracker.Domain.Entities.Shared;
using Domain.ValueObjects;

namespace Domain.BookTracker.Domain.Entities;

public class Tag : BaseEntity
{
    public TagName Name { get; set; }

    public Tag(string name)
    {
        Name = TagName.Create(name);
    }
}