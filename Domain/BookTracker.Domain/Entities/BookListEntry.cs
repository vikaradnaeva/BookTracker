using BookTracker.Domain.Base;

namespace BookTracker.Domain.Entities;

public enum BookListType
{
    Favorites, 
    Planned,  
    Read      
}

public class BookListEntry : Entity
{
    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public BookListType ListType { get; private set; }
    public DateTime AddedAt { get; private set; }

    public BookListEntry(Guid userId, Guid bookId, BookListType listType) : base()
    {
        UserId = userId;
        BookId = bookId;
        ListType = listType;
        AddedAt = DateTime.UtcNow;
    }

    public void ChangeListType(BookListType newListType)
    {
        ListType = newListType;
    }
}
