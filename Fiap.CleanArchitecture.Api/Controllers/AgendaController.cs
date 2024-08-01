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


        [Authorize]
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
