// ------------------------------------------------------------------------------------------------------
// <copyright file="IAgendaRepository.cs" company="TJ Sistemas">
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

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IAgendaRepository
    {
        public int CrieAgendaDoMedico(AgendaMedicoMes agenda);

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico);
    }
}
