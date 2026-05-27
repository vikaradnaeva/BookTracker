using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookRatingsController(IBookRatingRepository bookRatingRepository) : ControllerBase
{
    [HttpGet("book/{bookId:guid}/average")]
    public async Task<IActionResult> GetAverageRating(Guid bookId, CancellationToken cancellationToken)
    {
        var average = await bookRatingRepository.GetAverageRatingAsync(bookId, cancellationToken);
        return Ok(new { bookId, averageRating = average });
    }

    [HttpGet("user/{userId:guid}/book/{bookId:guid}")]
    public async Task<IActionResult> GetByUserAndBook(Guid userId, Guid bookId, CancellationToken cancellationToken)
    {
        var rating = await bookRatingRepository.GetByUserAndBookAsync(userId, bookId, cancellationToken);
        if (rating is null) return NotFound();
        return Ok(new { rating.Id, rating.UserId, rating.BookId, rating.Rating, rating.CreatedAt, rating.UpdatedAt });
    }
}
