using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CalendarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendars()
        {
            return await _context.Calendars.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);
            if (calendar == null)
                return NotFound();
            return calendar;
        }

        [HttpPost]
        public async Task<ActionResult<Calendar>> CreateCalendar(Calendar calendar)
        {
            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCalendar), new { id = calendar.CalendarId }, calendar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalendar(int id, Calendar calendar)
        {
            if (id != calendar.CalendarId)
                return BadRequest();
            _context.Entry(calendar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);
            if (calendar == null)
                return NotFound();
            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
