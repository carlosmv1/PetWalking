using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ZoneController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
        {
            return await _context.Zones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZone(int id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
                return NotFound();
            return zone;
        }

        [HttpPost]
        public async Task<ActionResult<Zone>> CreateZone(Zone zone)
        {
            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetZone), new { id = zone.ZoneId }, zone);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZone(int id, Zone zone)
        {
            if (id != zone.ZoneId)
                return BadRequest();
            _context.Entry(zone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(int id)
        {
            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
                return NotFound();
            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
