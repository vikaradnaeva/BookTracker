namespace BookTracker.Domain.Entities;

/// <summary>
/// Статус книги в списке пользователя (Избранное / В планах / Прочитано).
/// </summary>
public enum ReadingStatus
{
    Favourite,   // Избранное
    Planned,     // В планах
    Read         // Прочитано
}
