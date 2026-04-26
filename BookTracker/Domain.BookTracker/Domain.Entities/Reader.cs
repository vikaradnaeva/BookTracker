using Domain.BookTracker.Domain.Entities.Shared;
using Domain.BookTracker.Domain.Enums;
using Domain.ValueObjects;

namespace Domain.BookTracker.Domain.Entities;

public class Reader : BaseEntity
{
    public ReaderName Name { get; set; }
    public Dictionary<int, BookStatus> Books { get; set; } = new();
    public Dictionary<int, int> ReadingProgress { get; set; } = new();

    public Reader(string name)
    {
        Name = ReaderName.Create(name);
    }

    public void AddBook(int bookId, BookStatus status)
    {
        Books[bookId] = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveBook(int bookId)
    {
        Books.Remove(bookId);
        ReadingProgress.Remove(bookId);
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(int bookId, BookStatus newStatus)
    {
        if (Books.ContainsKey(bookId))
        {
            Books[bookId] = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void SaveProgress(int bookId, int chapter)
    {
        if (chapter < 0)
            throw new ArgumentException("Глава не может быть отрицательной.");

        ReadingProgress[bookId] = chapter;
        UpdatedAt = DateTime.UtcNow;
    }

    public int GetProgress(int bookId)
    {
        return ReadingProgress.GetValueOrDefault(bookId, 0);
    }

    public BookStatus? GetBookStatus(int bookId)
    {
        return Books.GetValueOrDefault(bookId);
    }
}