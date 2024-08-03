using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Entity.DAOs.Email;
using Fiap.CleanArchitecture.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailControlador _emailControlador;

        public EmailController(IEmailControlador emailControlador)
        {
            _emailControlador = emailControlador;
        }

        [Authorize]
        [VersaoApi("V1.0")]
        [HttpPost("enviar-email")]
        public async Task<IActionResult> SendMail([FromBody] EmailDAO mail)
        {
            try
            {
                await _emailControlador.SendMail(mail);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
