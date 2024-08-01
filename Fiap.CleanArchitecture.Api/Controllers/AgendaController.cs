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
        /// Busca as agendas do médico pelo seu Id
        /// </summary>
        /// <param name="id">Id do médico</param>
        /// <returns></returns>
        [Authorize]
        [Papel(["Paciente", "Medico"])]
        [VersaoApi("V1.0")]
        [HttpGet("buscar-por-medico-id/{id:int}")]
        public IActionResult BuscarPorId(int id)
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


    }
}
