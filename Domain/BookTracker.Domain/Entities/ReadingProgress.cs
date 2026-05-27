using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;

namespace BookTracker.Domain.Entities;

public class ReadingProgress : Entity<Guid>
{
    public Guid UserId { get; }
    public Book Book { get; }
    public int CurrentChapter { get; private set; }
    public DateTime StartedAt { get; }
    public DateTime LastReadAt { get; private set; }

    public ReadingProgress(Guid id, Guid userId, Book book, DateTime startedAt) : base(id)
    {
        UserId = userId;
        Book = book ?? throw new ArgumentNullValueException(nameof(book));
        CurrentChapter = 1;
        StartedAt = startedAt;
        LastReadAt = startedAt;
    }

    protected ReadingProgress() { }

    public void UpdateChapter(int chapter)
    {
        if (chapter < 1 || chapter > Book.TotalChapters)
            throw new InvalidChapterException(chapter, Book.TotalChapters);
        CurrentChapter = chapter;
        LastReadAt = DateTime.UtcNow;
    }
}
