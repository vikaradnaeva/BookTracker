using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingProgressController(IReadingProgressRepository readingProgressRepository) : ControllerBase
{
    [HttpGet("user/{userId:guid}/book/{bookId:guid}")]
    public async Task<IActionResult> GetByUserAndBook(Guid userId, Guid bookId, CancellationToken cancellationToken)
    {
        var progress = await readingProgressRepository.GetByUserAndBookAsync(userId, bookId, cancellationToken);
        if (progress is null) return NotFound();
        return Ok(new
        {
            progress.Id,
            progress.UserId,
            BookId = progress.Book.Id,
            BookTitle = progress.Book.Title.Value,
            progress.CurrentChapter,
            TotalChapters = progress.Book.TotalChapters,
            progress.StartedAt,
            progress.LastReadAt
        });
    }
}
