using Domain.BookTracker.Domain.Entities.Shared;
using Domain.ValueObjects;

namespace Domain.BookTracker.Domain.Entities;

public class Moderator : BaseEntity
{
    public ModeratorName Name { get; set; }
    public bool IsActive { get; set; } = true;
    public List<int> ModeratedBookIds { get; set; } = new();

    public Moderator(string name)
    {
        Name = ModeratorName.Create(name);
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddModeratedBook(int bookId)
    {
        if (!ModeratedBookIds.Contains(bookId))
        {
            ModeratedBookIds.Add(bookId);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveModeratedBook(int bookId)
    {
        ModeratedBookIds.Remove(bookId);
        UpdatedAt = DateTime.UtcNow;
    }
}