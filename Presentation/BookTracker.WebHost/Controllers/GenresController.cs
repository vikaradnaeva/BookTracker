using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(IGenreRepository genreRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var genres = await genreRepository.GetAllAsync(cancellationToken, asNoTracking: true);
        return Ok(genres.Select(g => new { g.Id, Name = g.Name.Value }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var genre = await genreRepository.GetByIdAsync(id, cancellationToken);
        if (genre is null) return NotFound();
        return Ok(new { genre.Id, Name = genre.Name.Value });
    }
}
