using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using PetWalkingApi.Models;
using Microsoft.Extensions.Configuration;

namespace PetWalkingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] ContactFormModel form)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            var recipient = _configuration["EmailSettings:Recipient"];
            var portString = _configuration["EmailSettings:Port"];

            if (string.IsNullOrWhiteSpace(smtpServer) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(recipient) ||
                !int.TryParse(portString, out int port))
            {
                return StatusCode(500, new { message = "Faltan o son inválidos los valores de configuración del correo." });
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(username));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = "Nuevo mensaje de contacto desde HappyPaws";

            email.Body = new TextPart(TextFormat.Plain)
            {
                Text = $"Nombre: {form.Name}\nCorreo: {form.Email}\nMensaje:\n{form.Message}"
            };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(username, password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return Ok(new { message = "Correo enviado correctamente" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el correo", error = ex.Message });
            }
        }
    }
}
