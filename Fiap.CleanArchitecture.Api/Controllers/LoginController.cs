using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly LoginControlador _loginControlador;

        public LoginController(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _loginControlador = new LoginControlador(_databaseClient);
        }

        [HttpPost]
        public IActionResult Autenticar([FromBody] UsuarioDAO usuarioDAO)
        {
            try
            {
                var usuario = new Usuario(usuarioDAO.Email, usuarioDAO.Senha);

                if (false/*validações*/)
                    throw new InvalidDataException("Formato dos dados incorreto!");

                var token = _loginControlador.GerarToken(usuario);

                return Ok(new { token });
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
