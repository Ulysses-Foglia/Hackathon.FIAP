using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Pessoa;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly PessoaControlador _pessoaControlador;

        public PessoaController(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _pessoaControlador = new PessoaControlador(_databaseClient);
        }

        [Authorize]
        [HttpGet("buscar-todos")]
        public IActionResult BuscarTodos()
        {
            try
            {
                var pessoas = _pessoaControlador.BuscarTodos();

                return Ok(pessoas);
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
                var pessoa = _pessoaControlador.BuscarPorId(id);

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("criar")]
        public IActionResult Criar([FromBody] PessoaDAO pessoaDAO)
        {
            try
            {
                _pessoaControlador.Criar(pessoaDAO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("alterar")]
        public IActionResult Alterar([FromBody] PessoaAlterarDAO pessoaAlterarDAO)
        {
            try
            {
                var novaPessoa = _pessoaControlador.Alterar(pessoaAlterarDAO);

                return Ok(novaPessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("excluir/{id:int}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _pessoaControlador.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
