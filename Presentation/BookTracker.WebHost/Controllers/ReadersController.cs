using BookTracker.Domain.Entities;
using BookTracker.Domain.Repositories.Abstractions;
using BookTracker.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadersController(
    IReaderRepository readerRepository,
    IBookRepository bookRepository,
    IBookRatingRepository bookRatingRepository,
    IReadingProgressRepository readingProgressRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var readers = await readerRepository.GetAllAsync(cancellationToken, asNoTracking: true);
        return Ok(readers.Select(r => new { r.Id, Username = r.Username.Value }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var reader = await readerRepository.GetWithDetailsAsync(id, cancellationToken);
        if (reader is null) return NotFound();
        return Ok(new
        {
            reader.Id,
            Username = reader.Username.Value,
            BookList = reader.BookList.Select(kv => new { BookId = kv.Key, Status = kv.Value.ToString() }),
        });
    }

    public record CreateReaderRequest(string Username);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReaderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var reader = new Reader(Guid.NewGuid(), new AuthorName(request.Username));
            var created = await readerRepository.AddAsync(reader, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created!.Id },
                new { created.Id, Username = created.Username.Value });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record AddToListRequest(Guid BookId, string Status);

    [HttpPost("{id:guid}/list")]
    public async Task<IActionResult> AddToList(Guid id, [FromBody] AddToListRequest request, CancellationToken cancellationToken)
    {
        var reader = await readerRepository.GetWithDetailsAsync(id, cancellationToken);
        if (reader is null) return NotFound("Reader not found.");

        var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        if (!Enum.TryParse<ReadingStatus>(request.Status, out var status))
            return BadRequest(new { error = $"Invalid status. Valid values: {string.Join(", ", Enum.GetNames<ReadingStatus>())}" });

        reader.AddToList(book, status);
        await readerRepository.UpdateAsync(reader, cancellationToken);
        return Ok(new { message = "Book added to list.", BookId = request.BookId, Status = status.ToString() });
    }

    public record RateBookRequest(Guid BookId, int Rating);

    [HttpPost("{id:guid}/rate")]
    public async Task<IActionResult> RateBook(Guid id, [FromBody] RateBookRequest request, CancellationToken cancellationToken)
    {
        var reader = await readerRepository.GetWithDetailsAsync(id, cancellationToken);
        if (reader is null) return NotFound("Reader not found.");

        var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        try
        {
            var rating = reader.RateBook(book, request.Rating);
            await bookRatingRepository.AddAsync(rating, cancellationToken);
            await readerRepository.UpdateAsync(reader, cancellationToken);
            return Ok(new { rating.Id, rating.Rating, rating.CreatedAt });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    public record StartReadingRequest(Guid BookId);

    [HttpPost("{id:guid}/reading/start")]
    public async Task<IActionResult> StartReading(Guid id, [FromBody] StartReadingRequest request, CancellationToken cancellationToken)
    {
        var reader = await readerRepository.GetWithDetailsAsync(id, cancellationToken);
        if (reader is null) return NotFound("Reader not found.");

        var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        var progress = reader.StartReading(book);
        await readingProgressRepository.AddAsync(progress, cancellationToken);
        await readerRepository.UpdateAsync(reader, cancellationToken);
        return Ok(new { progress.Id, progress.CurrentChapter, progress.StartedAt });
    }

    public record UpdateChapterRequest(Guid BookId, int Chapter);

    [HttpPatch("{id:guid}/reading/chapter")]
    public async Task<IActionResult> UpdateChapter(Guid id, [FromBody] UpdateChapterRequest request, CancellationToken cancellationToken)
    {
        var reader = await readerRepository.GetWithDetailsAsync(id, cancellationToken);
        if (reader is null) return NotFound("Reader not found.");

        var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
        if (book is null) return NotFound("Book not found.");

        try
        {
            reader.UpdateReadingChapter(book, request.Chapter);
            await readerRepository.UpdateAsync(reader, cancellationToken);
            return Ok(new { message = "Chapter updated.", Chapter = request.Chapter });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
