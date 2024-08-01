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
    public class MedicoController: ControllerBase, IMedicoController
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly IMedicoControlador _medicoControlador;

        public MedicoController(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _medicoControlador = new MedicoControlador(_databaseClient);
        }

        [VersaoApi("V1.0")]
        [HttpPost("autenticar-medico")]
        public IActionResult AutenticarMedico([FromBody] AutenticacaoModelDAO dados)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Dados fora do padão esperado"); }

                var token = _medicoControlador.GerarToken(new MedicoDAO() { Email = dados.Email, Senha = dados.Senha});

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
                var medicos = _medicoControlador.BuscarTodos();

                return Ok(medicos);
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
                var medicos = _medicoControlador.BuscarPorId(id);

                return Ok(medicos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [VersaoApi("V1.0")]
        [HttpPost("criar")]
        public IActionResult Criar([FromBody] MedicoDAO medicoDAO)
        {
            try
            {
                _medicoControlador.Criar(medicoDAO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize]
        [Papel("Medico")]
        [VersaoApi("V1.0")]
        [HttpPut("alterar")]
        public IActionResult Alterar([FromBody] MedicoAlterarDAO medicoAlterarDAO)
        {
            try
            {
                var novoMedico = _medicoControlador.Alterar(medicoAlterarDAO);

                return Ok(novoMedico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [Papel("Medico")]
        [VersaoApi("V1.0")]
        [HttpDelete("excluir/{id:int}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _medicoControlador.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
