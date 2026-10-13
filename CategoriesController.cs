using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public CategoriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet("api/categories")]
        public async Task<ActionResult<IEnumerable<EventCategory>>> GetCategories()
        {
            return await _context.EventCategories.ToListAsync();
        }

        // GET: api/categories/5
        [HttpGet("api/categories/{id}")]
        public async Task<ActionResult<EventCategory>> GetCategory(int id)
        {
            var category = await _context.EventCategories.FindAsync(id);
            if (category == null) return NotFound();
            return category;
        }

        // GET: api/events/5/categories
        [HttpGet("api/events/{eventId}/categories")]
        public async Task<ActionResult<IEnumerable<EventCategory>>> GetCategoriesByEvent(int eventId)
        {
            return await _context.EventCategories.Where(c => c.EventId == eventId).ToListAsync();
        }

        // POST: api/events/5/categories
        [HttpPost("api/events/{eventId}/categories")]
        public async Task<ActionResult<EventCategory>> PostCategory(int eventId, EventCategory category)
        {
            category.EventId = eventId;
            _context.EventCategories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // PUT: api/categories/5
        [HttpPut("api/categories/{id}")]
        public async Task<IActionResult> PutCategory(int id, EventCategory category)
        {
            if (id != category.CategoryId) return BadRequest();
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        // DELETE: api/categories/5
        [HttpDelete("api/categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.EventCategories.FindAsync(id);
            if (category == null) return NotFound();
            _context.EventCategories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}