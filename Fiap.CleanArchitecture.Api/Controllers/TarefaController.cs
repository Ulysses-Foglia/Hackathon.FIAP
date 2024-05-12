using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {   
        private readonly IDatabaseClient _databaseClient;        
        private readonly IControladorFactory<TarefaControlador> _controladorFactory;
        private ITarefaControlador _tarefaControlador;

        public TarefaController(IDatabaseClient databaseClient, IControladorFactory<TarefaControlador> tarefaControladorFactory)
        {
            _databaseClient = databaseClient;
            _controladorFactory = tarefaControladorFactory;
            _tarefaControlador = _controladorFactory.CriarControlador(_databaseClient);
        }

        [Authorize]
        [HttpGet("buscar-todos")]
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [VersaoApi(VersaoDaApi = "V1.0")]
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
        [HttpPost("alterar-situacao")]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult AlterarSituacao([FromBody] TarefaAlterarSituacaoDAO tarefaAlterarSituacaoDAO)
        {
            if (!ModelState.IsValid) { return StatusCode(500); }

            try
            {
                var novaTarefa = _tarefaControlador.AlterarSituacao(tarefaAlterarSituacaoDAO.Id, tarefaAlterarSituacaoDAO.Status);

                return Ok(novaTarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("atribuir-responsavel")]
        [VersaoApi(VersaoDaApi = "V1.0")]
        public IActionResult AtribuirResponsavel([FromBody] TarefaAtribuirResponsavelDAO tarefaAtribuirResponsavelDAO)
        {
            if (!ModelState.IsValid) { return StatusCode(500); }

            try
            {
                var novaTarefa = _tarefaControlador.AtribuaUmNovoResponsavel(tarefaAtribuirResponsavelDAO.Id, tarefaAtribuirResponsavelDAO.Status, tarefaAtribuirResponsavelDAO.IdResponsavel);

                return Ok(novaTarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("excluir/{id:int}")]
        [VersaoApi(VersaoDaApi = "V1.0")]
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
