using BookTracker.Domain.Entities;
using BookTracker.Domain.Exceptions;
using BookTracker.Domain.Repositories.Abstractions;
using BookTracker.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(
    IBookRepository bookRepository,
    IGenreRepository genreRepository,
    ITagRepository tagRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetAllAsync(cancellationToken, asNoTracking: true);
        return Ok(books.Select(b => new
        {
            b.Id,
            Title = b.Title.Value,
            Author = b.Author.Value,
            b.TotalChapters,
            Genres = b.Genres.Select(g => new { g.Id, Name = g.Name.Value }),
            Tags = b.Tags.Select(t => new { t.Id, Name = t.Name.Value })
        }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(id, cancellationToken);
        if (book is null) return NotFound();
        return Ok(new
        {
            book.Id,
            Title = book.Title.Value,
            Author = book.Author.Value,
            book.TotalChapters,
            Genres = book.Genres.Select(g => new { g.Id, Name = g.Name.Value }),
            Tags = book.Tags.Select(t => new { t.Id, Name = t.Name.Value })
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string title, CancellationToken cancellationToken)
    {
        var books = await bookRepository.FindByTitleAsync(title, cancellationToken);
        return Ok(books.Select(b => new { b.Id, Title = b.Title.Value, Author = b.Author.Value }));
    }

    [HttpGet("genre/{genreId:guid}")]
    public async Task<IActionResult> GetByGenre(Guid genreId, CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetByGenreAsync(genreId, cancellationToken);
        return Ok(books.Select(b => new { b.Id, Title = b.Title.Value, Author = b.Author.Value }));
    }

    [HttpGet("tag/{tagId:guid}")]
    public async Task<IActionResult> GetByTag(Guid tagId, CancellationToken cancellationToken)
    {
        var books = await bookRepository.GetByTagAsync(tagId, cancellationToken);
        return Ok(books.Select(b => new { b.Id, Title = b.Title.Value, Author = b.Author.Value }));
    }
}
