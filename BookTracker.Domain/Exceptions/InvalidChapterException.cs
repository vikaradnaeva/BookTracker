namespace BookTracker.Domain.Exceptions;

public class InvalidChapterException : DomainException
{
    public int AttemptedChapter { get; }
    public int TotalChapters { get; }

    public InvalidChapterException(int chapter, int totalChapters)
        : base($"Chapter '{chapter}' is invalid. Must be between 0 and {totalChapters}.")
    {
        AttemptedChapter = chapter;
        TotalChapters = totalChapters;
    }
}
