// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaController.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Attribute;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendaController : ControllerBase
    {

        private readonly IDatabaseClient _databaseClient;
        private readonly IAgendaControlador _agendaControlador;

        public AgendaController(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _agendaControlador = new AgendaControlador(_databaseClient);
        }

        /// <summary>
        /// Cria uma nova agenda do médico
        /// </summary>
        /// <param name="angendaDAO">Seguir o Json de amostra do Swagger</param>
        /// <returns></returns>
        [Authorize]
        [Papel("Medico")]
        [VersaoApi("V1.0")]
        [HttpPost("criar")]
        public IActionResult CriarAgendaMedico([FromBody] AgendaMedicoMesDAO angendaDAO)
        {
            try
            {
                _agendaControlador.CrieAgendaDoMedico(angendaDAO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Cria um horario na agenda do médico
        /// </summary>
        /// <param name="agendaMedicoDiaDAO">Seguir o Json de amostra do Swagger</param>
        /// <returns></returns>
        [Authorize]
        [Papel("Medico")]
        [VersaoApi("V1.0")]
        [HttpPost("criar-horario-agenda")]
        public IActionResult CriarHorarioAgendaMedico([FromBody] AgendaMedicoDiaDAO agendaMedicoDiaDAO)
        {
            try
            {
                var resposta = _agendaControlador.CrieHorarioNaAgendaDoMedico(agendaMedicoDiaDAO);

                return Ok(resposta != 0 ? $"Foi criado um horário com código {resposta}" : "Não foi possível criar o horário na agenda");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Atualiza a agenda do dia do médico
        /// </summary>
        /// <param name="dados">Id da agenda e a disponibilidade (DISPONIVEL OU INDISPONIVEL)</param>
        /// <returns></returns>
        [Authorize]
        [Papel("Medico")]
        [VersaoApi("V1.0")]
        [HttpPost("atualizar-diponibilidade-agenda")]
        public IActionResult AtualizeDiponibilidadeAgenda([FromBody] AgendaMedicoAtualizarDisponibilidadeDAO dados)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Formato invalido dos dados"); };

                dados.ValideEntradaDeUsuario();

                var agenda = _agendaControlador.AtualizeAhDisponibilidadeDaAgendaDoMedico(dados.AgendaId, dados.Disponibilidade);

                return Ok(agenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Atualiza um horário específico da agenda mudando a sua hora e deixando como 'Disponível'
        /// </summary>
        /// <param name="dados">Seguir o Json de amostra do Swagger</param>
        /// <returns></returns>
        [Authorize]
        [Papel("Medico")]
        [VersaoApi("V1.0")]
        [HttpPost("atualizar-horario-agenda")]
        public IActionResult AtualizeHorarioAgenda([FromBody] AgendaMedicoAtualizeHorarioDAO dados)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Formato invalido dos dados"); };

                dados.ValideEntradaDoUsuario();

                var agenda = _agendaControlador.AtualizeHorarioDaAgenda(dados);

                return Ok(agenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Agenda um paciente em determinada agenda e horário marcando a mesma como 'Indisponível'
        /// </summary>
        /// <param name="dados">Seguir o Json de modelo do Swagger</param>
        /// <returns></returns>
        [Authorize]
        [Papel(["Paciente", "Medico"])]
        [VersaoApi("V1.0")]
        [HttpPost("agende-horario-paciente")]
        public IActionResult AgendePaciente([FromBody] AgendaMedicoAgendarPacienteDAO dados)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Formato invalido dos dados"); };

                dados.ValideEntradaDeUsuario();

                var agenda = _agendaControlador.AtualizeHorarioDaAgendaComPaciente(dados);

                return Ok(agenda != 0 ? "Paciente agendado." : "Paciente não agendado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Buca a agenda do médico informando o id, dia, mes e ano na consulta.
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        [Authorize]
        [Papel(["Paciente", "Medico"])]
        [VersaoApi("V1.0")]
        [HttpPost("busque-por-id-medico-dia-mesano")]
        public IActionResult BusqueAgendaPeloFiltro([FromBody] AgendaMedicoFiltroIdMedicoDiaMesAnoDAO dados)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Formato invalido dos dados"); };

                dados.ValideEntradaDoUsuario();

                var agenda = _agendaControlador.BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(dados);

                return StatusCode(200, agenda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Busca as agendas do médico pelo seu Id
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <returns></returns>
        [Authorize]
        [Papel(["Paciente", "Medico"])]
        [VersaoApi("V1.0")]
        [HttpGet("buscar-por-id/{id:int}")]
        public IActionResult BuscarTodasAgendasPorMedicoId(int id)
        {
            try
            {
                var usuarios = _agendaControlador.BusqueTodasAgendasDoMedico(id);

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete a agenda e os horários à ela relacionados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Papel(["Medico"])]
        [VersaoApi("V1.0")]
        [HttpDelete("delete-agenda-por-id/{id:int}")]
        public IActionResult DeleteAgendaPeloId(int id) 
        {
            try
            {
                var resposta = _agendaControlador.RemovaAgendaEhHorarioDaAgenda(id);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deleta o horario passado no parâmetro
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        [Authorize]
        [Papel(["Medico"])]
        [VersaoApi("V1.0")]
        [HttpDelete("delete-horario")]
        public IActionResult DeleteHorarioDaAgenda([FromBody] AgendaMedicoFiltroExclusaoHorarioDAO dados)
        {
            try
            {
                if (!ModelState.IsValid) { throw new Exception("Formato invalido dos dados"); };

                dados.ValideEntradaDoUsuario();

                var resposta = _agendaControlador.RemovaHorarioDaAgenda(dados);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
