using Big_Api.Data;
using Big_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Big_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AppEntityController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public AppEntityController(ApplicationDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<AppEntity>>> GetYourEntities()
  {
    return await _context.appEntities.ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<AppEntity>> GetYourEntity(int id)
  {
    var entity = await _context.appEntities.FindAsync(id);

    if (entity == null)
    {
      return NotFound();
    }

    return entity;
  }

  [HttpPost]
  public async Task<ActionResult<AppEntity>> PostYourEntity(AppEntity entity)
  {
    _context.appEntities.Add(entity);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetYourEntity), new { id = entity.Id }, entity);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> PutYourEntity(int id, AppEntity entity)
  {
    if (id != entity.Id)
    {
      return BadRequest();
    }

    _context.Entry(entity).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteYourEntity(int id)
  {
    var entity = await _context.appEntities.FindAsync(id);
    if (entity == null)
    {
      return NotFound();
    }

    _context.appEntities.Remove(entity);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}

