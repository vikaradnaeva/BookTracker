using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;

namespace BookTracker.Domain.Entities;

public class ReadingProgress : Entity
{
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public int CurrentChapter { get; private set; }
    public int TotalChapters { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime LastReadAt { get; private set; }

    public double ProgressPercentage =>
        TotalChapters == 0 ? 0 : (double)CurrentChapter / TotalChapters * 100;

    public ReadingProgress(Guid userId, Guid bookId, int totalChapters) : base()
    {
        UserId = userId;
        BookId = bookId;
        TotalChapters = totalChapters;
        CurrentChapter = 0;
        StartedAt = DateTime.UtcNow;
        LastReadAt = DateTime.UtcNow;
    }

    public void UpdateChapter(int chapter)
    {
        if (chapter < 0 || chapter > TotalChapters)
            throw new InvalidChapterException(chapter, TotalChapters);

        CurrentChapter = chapter;
        LastReadAt = DateTime.UtcNow;
    }
}
