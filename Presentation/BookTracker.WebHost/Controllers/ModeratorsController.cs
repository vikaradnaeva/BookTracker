using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using BookTracker.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModeratorsController(
    IModeratorRepository moderatorRepository,
    IBookRepository bookRepository,
    IGenreRepository genreRepository,
    ITagRepository tagRepository) : ControllerBase
{
    public record CreateModeratorRequest(string Username);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModeratorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var moderator = new Moderator(Guid.NewGuid(), new AuthorName(request.Username));
            var created = await moderatorRepository.AddAsync(moderator, cancellationToken);
            return Ok(new { created!.Id, Username = created.Username.Value });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record AddBookRequest(string Title, string Author, int TotalChapters);

    [HttpPost("{moderatorId:guid}/books")]
    public async Task<IActionResult> AddBook(Guid moderatorId, [FromBody] AddBookRequest request, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        try
        {
            var catalogue = (await bookRepository.GetAllAsync(cancellationToken)).ToList();
            var book = moderator.AddBook(catalogue, new BookTitle(request.Title), new AuthorName(request.Author), request.TotalChapters);
            var created = await bookRepository.AddAsync(book, cancellationToken);
            return CreatedAtAction("GetById", "Books", new { id = created!.Id },
                new { created.Id, Title = created.Title.Value, Author = created.Author.Value, created.TotalChapters });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record DeleteBookRequest(Guid BookId);

    [HttpDelete("{moderatorId:guid}/books/{bookId:guid}")]
    public async Task<IActionResult> DeleteBook(Guid moderatorId, Guid bookId, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        var catalogue = (await bookRepository.GetAllAsync(cancellationToken)).ToList();
        moderator.DeleteBook(catalogue, book);
        await bookRepository.DeleteAsync(book, cancellationToken);
        return NoContent();
    }

    public record CreateGenreRequest(string Name);

    [HttpPost("{moderatorId:guid}/genres")]
    public async Task<IActionResult> CreateGenre(Guid moderatorId, [FromBody] CreateGenreRequest request, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        try
        {
            var genre = moderator.CreateGenre(new GenreName(request.Name));
            var created = await genreRepository.AddAsync(genre, cancellationToken);
            return Ok(new { created!.Id, Name = created.Name.Value });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record CreateTagRequest(string Name);

    [HttpPost("{moderatorId:guid}/tags")]
    public async Task<IActionResult> CreateTag(Guid moderatorId, [FromBody] CreateTagRequest request, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        try
        {
            var tag = moderator.CreateTag(new TagName(request.Name));
            var created = await tagRepository.AddAsync(tag, cancellationToken);
            return Ok(new { created!.Id, Name = created.Name.Value });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record AddGenreToBookRequest(Guid BookId, Guid GenreId);

    [HttpPost("{moderatorId:guid}/books/{bookId:guid}/genres/{genreId:guid}")]
    public async Task<IActionResult> AddGenreToBook(Guid moderatorId, Guid bookId, Guid genreId, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        var genre = await genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre is null) return NotFound("Genre not found.");

        moderator.AddGenreToBook(book, genre);
        await bookRepository.UpdateAsync(book, cancellationToken);
        return Ok(new { message = "Genre added to book." });
    }

    [HttpDelete("{moderatorId:guid}/books/{bookId:guid}/genres/{genreId:guid}")]
    public async Task<IActionResult> RemoveGenreFromBook(Guid moderatorId, Guid bookId, Guid genreId, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        var genre = await genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre is null) return NotFound("Genre not found.");

        moderator.RemoveGenreFromBook(book, genre);
        await bookRepository.UpdateAsync(book, cancellationToken);
        return NoContent();
    }

    [HttpPost("{moderatorId:guid}/books/{bookId:guid}/tags/{tagId:guid}")]
    public async Task<IActionResult> AddTagToBook(Guid moderatorId, Guid bookId, Guid tagId, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        var tag = await tagRepository.GetByIdAsync(tagId, cancellationToken);
        if (tag is null) return NotFound("Tag not found.");

        moderator.AddTagToBook(book, tag);
        await bookRepository.UpdateAsync(book, cancellationToken);
        return Ok(new { message = "Tag added to book." });
    }

    [HttpDelete("{moderatorId:guid}/books/{bookId:guid}/tags/{tagId:guid}")]
    public async Task<IActionResult> RemoveTagFromBook(Guid moderatorId, Guid bookId, Guid tagId, CancellationToken cancellationToken)
    {
        var moderator = await moderatorRepository.GetByIdAsync(moderatorId, cancellationToken);
        if (moderator is null) return NotFound("Moderator not found.");

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        var tag = await tagRepository.GetByIdAsync(tagId, cancellationToken);
        if (tag is null) return NotFound("Tag not found.");

        moderator.RemoveTagFromBook(book, tag);
        await bookRepository.UpdateAsync(book, cancellationToken);
        return NoContent();
    }
}
