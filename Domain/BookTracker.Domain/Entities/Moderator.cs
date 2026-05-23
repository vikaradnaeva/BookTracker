using BookTracker.Domain.Base;
using BookTracker.Domain.Exceptions;
using BookTracker.ValueObjects;

namespace BookTracker.Domain.Entities;

public class Moderator : Entity<Guid>
{
    public AuthorName Username { get; private set; }

    public Moderator(Guid id, AuthorName username) : base(id)
    {
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
    }

    protected Moderator() { }

    public void DeleteBook(ICollection<Book> catalogue, Book book)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        if (!catalogue.Contains(book))
            throw new BookNotFoundException(book.Id);
        catalogue.Remove(book);
    }

    public Book AddBook(ICollection<Book> catalogue, BookTitle title, AuthorName author, int totalChapters)
    {
        if (title == null) throw new ArgumentNullValueException(nameof(title));
        if (author == null) throw new ArgumentNullValueException(nameof(author));

        var book = new Book(Guid.NewGuid(), title, author, totalChapters);
        catalogue.Add(book);
        return book;
    }

    public void AddGenreToBook(Book book, Genre genre)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        if (genre == null) throw new ArgumentNullValueException(nameof(genre));
        book.AddGenre(genre);
    }

    public void RemoveGenreFromBook(Book book, Genre genre)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        if (genre == null) throw new ArgumentNullValueException(nameof(genre));
        book.RemoveGenre(genre);
    }

    public void AddTagToBook(Book book, Tag tag)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        if (tag == null) throw new ArgumentNullValueException(nameof(tag));
        book.AddTag(tag);
    }

    public void RemoveTagFromBook(Book book, Tag tag)
    {
        if (book == null) throw new ArgumentNullValueException(nameof(book));
        if (tag == null) throw new ArgumentNullValueException(nameof(tag));
        book.RemoveTag(tag);
    }

    public Genre CreateGenre(GenreName name)
    {
        if (name == null) throw new ArgumentNullValueException(nameof(name));
        return new Genre(Guid.NewGuid(), name);
    }

    public Tag CreateTag(TagName name)
    {
        if (name == null) throw new ArgumentNullValueException(nameof(name));
        return new Tag(Guid.NewGuid(), name);
    }

    public void RenameGenre(Genre genre, GenreName newName)
    {
        if (genre == null) throw new ArgumentNullValueException(nameof(genre));
        genre.Rename(newName);
    }

    public void RenameTag(Tag tag, TagName newName)
    {
        if (tag == null) throw new ArgumentNullValueException(nameof(tag));
        tag.Rename(newName);
    }
}
