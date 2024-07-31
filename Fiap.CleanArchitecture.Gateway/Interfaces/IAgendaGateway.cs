// ------------------------------------------------------------------------------------------------------
// <copyright file="IAgendaGateway.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface IAgendaGateway
    {
        public int CrieAgendaDoMedico(AgendaMedicoMes agenda);

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico);
    }
}
