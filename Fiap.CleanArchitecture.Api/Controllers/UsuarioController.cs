using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly UsuarioControlador _usuarioControlador;

        public UsuarioController(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _usuarioControlador = new(_databaseClient);
        }

        [Authorize]
        [HttpGet("buscar-todos")]
        public IActionResult Get()
        {
            var usuarios = _usuarioControlador.BuscarTodosUsuarios();

            return Ok(usuarios);
        }
    }
}
