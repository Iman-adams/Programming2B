using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return await _context.Results.Include(r => r.Enrollment).ToListAsync();
        }

        // GET: api/results/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Results.Include(r => r.Enrollment).FirstOrDefaultAsync(r => r.ResultId == id);
            if (result == null) return NotFound();
            return result;
        }

        // GET: api/results/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultsByCategory(int categoryId)
        {
            return await _context.Results
                .Include(r => r.Enrollment)
                .Where(r => r.Enrollment != null && r.Enrollment.CategoryId == categoryId)
                .OrderBy(r => r.OverallRank)
                .ToListAsync();
        }

        // POST: api/results
        [HttpPost]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetResult", new { id = result.ResultId }, result);
        }

        // PUT: api/results/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResult(int id, Result result)
        {
            if (id != result.ResultId) return BadRequest();
            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        // DELETE: api/results/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result == null) return NotFound();
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}