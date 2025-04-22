using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public PetController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

       // GET: api/pet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetActivePets()
        {
            return await _context.Pets
                .Where(p => p.Status == "A")
                .ToListAsync();
        }

        // GET: api/pet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDto>> GetPet(int id)
        {
            var pet = await (from p in _context.Pets
                            join b in _context.PetBreeds on p.PetBreedId equals b.PetBreedId
                            join t in _context.PetTypes on p.PetTypeId equals t.PetTypeId
                            where p.PetId == id && p.Status == "A"
                            select new PetDto
                            {
                                PetId = p.PetId,
                                Name = p.Name,
                                Gender = p.Gender,
                                Observations = p.Observations,
                                BreedName = b.BreedName,
                                PetTypeName = t.TypeName
                            }).FirstOrDefaultAsync();

            if (pet == null)
                return NotFound();

            return pet;
        }

        // POST: api/pet
        [HttpPost]
        public async Task<ActionResult<Pet>> CreatePet(Pet pet)
        {
            pet.Status = "A";

            var typeExists = await _context.PetTypes.AnyAsync(t => t.PetTypeId == pet.PetTypeId);
            var breedExists = await _context.PetBreeds.AnyAsync(b => b.PetBreedId == pet.PetBreedId);

            if (!typeExists || !breedExists)
                return BadRequest("PetTypeId o PetBreedId no válidos.");

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPet), new { id = pet.PetId }, pet);
        }

        // PUT: api/pet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(int id, Pet pet)
        {
            if (id != pet.PetId)
                return BadRequest();

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pets.Any(e => e.PetId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/pet/5 (eliminación lógica)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
                return NotFound();

            pet.Status = "I";
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

  // POST: api/pet/{id}/upload-image
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadPetImage(int id, IFormFile image)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
                return NotFound();

            if (image == null || image.Length == 0)
                return BadRequest("Imagen no válida.");

            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"pet_{id}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            pet.Image = $"/uploads/{fileName}";
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();

            return Ok(new { imageUrl = pet.Image });
        }
    }
}
