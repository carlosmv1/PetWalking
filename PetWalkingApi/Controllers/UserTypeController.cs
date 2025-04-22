using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserTypeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserTypes()
        {
            return await _context.UserTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
                return NotFound();
            return userType;
        }

        [HttpPost]
        public async Task<ActionResult<UserType>> CreateUserType(UserType userType)
        {
            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserType), new { id = userType.UserTypeId }, userType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserType(int id, UserType userType)
        {
            if (id != userType.UserTypeId)
                return BadRequest();
            _context.Entry(userType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
                return NotFound();
            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
