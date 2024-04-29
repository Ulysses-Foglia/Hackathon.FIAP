using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;
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

        [HttpPost]
        [Route("autenticar")]
        public IActionResult Autenticar([FromBody] UsuarioDAO usuarioDAO)
        {
            try
            {
                var usuario = new Usuario(usuarioDAO.Email, usuarioDAO.Senha);

                if (false/*validações*/)
                    throw new InvalidDataException("Formato dos dados incorreto!");

                var token = _usuarioControlador.GerarToken(usuario);

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

        [Authorize]
        [HttpGet("buscar-todos")]
        public IActionResult BuscarTodos()
        {
            var usuarios = _usuarioControlador.BuscarTodos();

            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("buscar-por-id/{id:int}")]
        public IActionResult BuscarPorId(int id)
        {
            var usuarios = _usuarioControlador.BuscarPorId(id);

            return Ok(usuarios);
        }

        [Authorize]
        [HttpPost("criar")]
        public IActionResult Criar([FromBody] UsuarioDAO usuarioDAO)
        {
            var usuario = new Usuario(usuarioDAO.Email, usuarioDAO.Senha);

            if (false/*validações*/)
                throw new InvalidDataException("Formato dos dados incorreto!");

            _usuarioControlador.Criar(usuario);

            return Ok();
        }

        [Authorize]
        [HttpPut("alterar")]
        public IActionResult Alterar([FromBody] UsuarioAlterarDAO usuarioAlterarDAO)
        {
            var usuario = new Usuario(usuarioAlterarDAO);

            if (false/*validações*/)
                throw new InvalidDataException("Formato dos dados incorreto!");

            var novousuario = _usuarioControlador.Alterar(usuario);

            return Ok(novousuario);
        }

        [Authorize]
        [HttpDelete("excluir/{id:int}")]
        public IActionResult Excluir(int id)
        {
            _usuarioControlador.Excluir(id);

            return Ok();
        }
    }
}
