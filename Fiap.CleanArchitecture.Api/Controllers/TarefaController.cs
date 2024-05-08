using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {   
        private readonly IDatabaseClient _databaseClient;        
        private readonly IControladorFactory<ITarefaControlador> _controladorFactory;
        private ITarefaControlador _tarefaControlador;

        public TarefaController(IDatabaseClient databaseClient, IControladorFactory<ITarefaControlador> tarefaControladorFactory)
        {
            _databaseClient = databaseClient;
            _controladorFactory = tarefaControladorFactory;
            _tarefaControlador = _controladorFactory.CriarControlador(_databaseClient);
        }

        [Authorize]
        [HttpGet("buscar-todos")]
        public IActionResult BuscarTodos()
        {
            try
            {
                var tarefas = _tarefaControlador.BuscarTodos();

                return Ok(tarefas);
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
                var tarefa = _tarefaControlador.BuscarPorId(id);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("criar")]
        public IActionResult Criar([FromBody] TarefaDAO tarefaDAO)
        {
            try
            {
                _tarefaControlador.Criar(tarefaDAO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("alterar")]
        public IActionResult Alterar([FromBody] TarefaAlterarDAO tarefaAlterarDAO)
        {
            try
            {
                var novaTarefa = _tarefaControlador.Alterar(tarefaAlterarDAO);

                return Ok(novaTarefa);
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
                _tarefaControlador.Excluir(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
