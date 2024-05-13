using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Attribute;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly IControladorFactory<UsuarioControlador> _controladorFactory;
        private IUsuarioControlador _usuarioControlador;

        public UsuarioController(IDatabaseClient databaseClient, IControladorFactory<UsuarioControlador> usuarioControladorFactory, IUsuarioControlador usuarioControlador)
        {
            _databaseClient = databaseClient;
            _controladorFactory = usuarioControladorFactory;         
            //var retorno = _controladorFactory.CriarControlador(_databaseClient);
            //var retornoControl = usuarioControlador;
            //_usuarioControlador = _controladorFactory.CriarControlador(_databaseClient);
            _usuarioControlador = new UsuarioControlador(_databaseClient) ; 
        }

        [HttpPost]
        [Route("autenticar")]
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
