// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaUseCase.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.UseCase
{
    public class AgendaUseCase : IAgendaUseCase
    {
        private readonly IAgendaGateway _agendaGateway;
        private readonly IMedicoGateway _medicoGateway;
        public AgendaUseCase(IAgendaGateway agendaGateway, IMedicoGateway medicoGateway)
        {
            _agendaGateway = agendaGateway;
            _medicoGateway = medicoGateway;
        }
        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda)
        {
                       

            var medicoMes = new AgendaMedicoMes(agenda, true);

            return _agendaGateway.CrieAgendaDoMedico(medicoMes);
        }



    }
}
