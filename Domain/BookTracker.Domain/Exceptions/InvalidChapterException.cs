namespace BookTracker.Domain.Exceptions;

public class InvalidChapterException(int chapter, int totalChapters)
    : ArgumentException($"Chapter {chapter} is invalid. Book has {totalChapters} chapters.");
