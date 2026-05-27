using BookTracker.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookTracker.WebHost.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController(ITagRepository tagRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetAllAsync(cancellationToken, asNoTracking: true);
        return Ok(tags.Select(t => new { t.Id, Name = t.Name.Value }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(id, cancellationToken);
        if (tag is null) return NotFound();
        return Ok(new { tag.Id, Name = tag.Name.Value });
    }
}
