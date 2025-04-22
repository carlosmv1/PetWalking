using Microsoft.AspNetCore.Mvc;
using PetWalkingApi.Data;
using PetWalkingApi.Models;

namespace PetWalkingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == request.UserName && u.Password == request.Password);

            if (user == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(new
            {
                user.UserId,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                user.UserTypeId
                // Si quieres puedes generar un token aquí más adelante
            });
        }
    }
}