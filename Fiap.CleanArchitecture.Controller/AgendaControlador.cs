// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaControlador.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.UseCase;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Controller
{
    public class AgendaControlador : IAgendaControlador
    {

        private readonly IDatabaseClient _databaseClient;
        private readonly IAgendaGateway _agendaGateway;
        private readonly IAgendaUseCase _agendaUseCase;

        public AgendaControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _agendaGateway = new AgendaGateway(_databaseClient);
            _agendaUseCase = new AgendaUseCase(_agendaGateway);
        }

        public string BusqueTodasAgendasDoMedico(int idMedico)
        {
            throw new NotImplementedException();
        }

        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda)
        {
            return _agendaUseCase.CrieAgendaDoMedico(agenda);
        }
    }
}
