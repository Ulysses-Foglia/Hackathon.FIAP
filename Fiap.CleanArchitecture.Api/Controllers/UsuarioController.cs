using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Attribute;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly IControladorFactory<IUsuarioControlador> _controladorFactory;
        private IUsuarioControlador _usuarioControlador;

        public UsuarioController(IDatabaseClient databaseClient, IControladorFactory<IUsuarioControlador> usuarioControladorFactory)
        {
            _databaseClient = databaseClient;
            _controladorFactory = usuarioControladorFactory;
            _usuarioControlador = _controladorFactory.CriarControlador(_databaseClient);
        }

        [HttpPost]
        [Route("autenticar")]
        public IActionResult Autenticar([FromBody] UsuarioDAO usuarioDAO)
        {
            try
            {
                
                var token = _usuarioControlador.GerarToken(usuarioDAO);

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
            try
            {
                var usuarios = _usuarioControlador.BuscarTodos();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("buscar-por-id/{id:int}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var usuarios = _usuarioControlador.BuscarPorId(id);

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [Authorize]
        [Papel("Admin")]
        [HttpPost("criar")]
        public IActionResult Criar([FromBody] UsuarioDAO usuarioDAO)
        {
            try
            {
                _usuarioControlador.Criar(usuarioDAO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [Authorize]
        [Papel("Admin")]
        [HttpPut("alterar")]
        public IActionResult Alterar([FromBody] UsuarioAlterarDAO usuarioAlterarDAO)
        {
            try
            {
                var novoUsuario = _usuarioControlador.Alterar(usuarioAlterarDAO);

                return Ok(novoUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [Authorize]
        [Papel("Admin")]
        [HttpDelete("excluir/{id:int}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _usuarioControlador.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }
    }
}
