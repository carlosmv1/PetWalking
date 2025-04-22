using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PetTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pettype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetType>>> GetPetTypes()
        {
            return await _context.PetTypes.ToListAsync();
        }

        // GET: api/pettype/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetType>> GetPetType(int id)
        {
            var petType = await _context.PetTypes.FindAsync(id);

            if (petType == null)
                return NotFound();

            return petType;
        }

        // POST: api/pettype
        [HttpPost]
        public async Task<ActionResult<PetType>> CreatePetType(PetType petType)
        {
            _context.PetTypes.Add(petType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPetType), new { id = petType.PetTypeId }, petType);
        }

        // PUT: api/pettype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePetType(int id, PetType petType)
        {
            if (id != petType.PetTypeId)
                return BadRequest();

            _context.Entry(petType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PetTypes.Any(pt => pt.PetTypeId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/pettype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetType(int id)
        {
            var petType = await _context.PetTypes.FindAsync(id);
            if (petType == null)
                return NotFound();

            _context.PetTypes.Remove(petType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
