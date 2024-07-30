using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Attribute;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
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
        private IUsuarioControlador _usuarioControlador;

        public UsuarioController(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;     
            _usuarioControlador = new UsuarioControlador(_databaseClient) ; 
        }

        [HttpPost]
        [VersaoApi("V1.0")]
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
        [VersaoApi("V1.0")]
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
        [VersaoApi("V1.0")]
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
        [VersaoApi("V1.0")]
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
        [VersaoApi("V1.0")]
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
        [VersaoApi("V1.0")]
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
