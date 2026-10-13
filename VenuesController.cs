using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [Route("api/venues")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public VenuesController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/venues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues()
        {
            return await _context.Venues.ToListAsync();
        }

        // GET: api/venues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            return venue;
        }

        // POST: api/venues
        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(Venue venue)
        {
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVenue", new { id = venue.VenueId }, venue);
        }

        // PUT: api/venues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(int id, Venue venue)
        {
            if (id != venue.VenueId) return BadRequest();
            _context.Entry(venue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(venue);
        }

        // DELETE: api/venues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) return NotFound();
            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}