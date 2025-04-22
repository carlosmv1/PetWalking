using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetBreedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PetBreedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/petbreed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetBreed>>> GetPetBreeds()
        {
            return await _context.PetBreeds.ToListAsync();
        }

        // GET: api/petbreed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetBreed>> GetPetBreed(int id)
        {
            var petBreed = await _context.PetBreeds.FindAsync(id);

            if (petBreed == null)
                return NotFound();

            return petBreed;
        }

        // POST: api/petbreed
        [HttpPost]
        public async Task<ActionResult<PetBreed>> CreatePetBreed(PetBreed petBreed)
        {
            _context.PetBreeds.Add(petBreed);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPetBreed), new { id = petBreed.PetBreedId }, petBreed);
        }

        // PUT: api/petbreed/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePetBreed(int id, PetBreed petBreed)
        {
            if (id != petBreed.PetBreedId)
                return BadRequest();

            _context.Entry(petBreed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PetBreeds.Any(pb => pb.PetBreedId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/petbreed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetBreed(int id)
        {
            var petBreed = await _context.PetBreeds.FindAsync(id);
            if (petBreed == null)
                return NotFound();

            _context.PetBreeds.Remove(petBreed);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
