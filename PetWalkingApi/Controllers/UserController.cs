using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Common;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UserController(AppDbContext context, IUserService userService, IWebHostEnvironment environment)
        {
            _context = context;
            _userService = userService;
            _environment = environment;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return user;
        }

       [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.Status = UserStatus.Inactive;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _userService.HandlePostUserCreationAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.UserId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/user/5
       [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // ⚠️ Inhabilitar en lugar de eliminar
            user.Status = "I";
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/user/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] string status)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Status = status;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Estado del usuario actualizado a '{status}'." });
        }

        // GET: api/user/status/A
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByStatus(string status)
        {
            var users = await _context.Users
                .Where(u => u.Status == status)
                .ToListAsync();

            if (users == null || users.Count == 0)
                return NotFound(new { message = $"No se encontraron usuarios con status '{status}'." });

            return Ok(users);
        }

        // GET: api/user/type/admin
        [HttpGet("type/{typeName}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByTypeName(string typeName)
        {
            var userType = await _context.UserTypes
                .FirstOrDefaultAsync(ut => ut.Type == typeName);

            if (userType == null)
                return NotFound(new { message = $"No existe el tipo de usuario '{typeName}'." });

            var users = await _context.Users
                .Where(u => u.UserTypeId == userType.UserTypeId)
                .ToListAsync();

            if (users == null || users.Count == 0)
                return NotFound(new { message = $"No se encontraron usuarios con tipo '{typeName}'." });

            return Ok(users);
        }

         // POST: api/user/{id}/upload-image
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadUserImage(int id, IFormFile image)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            if (image == null || image.Length == 0)
                return BadRequest("Imagen no válida.");

            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"user_{id}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            user.Image = $"/uploads/{fileName}";
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { imageUrl = user.Image });
        }
    }
}