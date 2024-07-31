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
        [HttpPost("criar-agenda-medico")]
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


    }
}
