// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoAgendadosDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaDeMedicosAgendadosDAO
    {
        public int Id { get; set; }

        public int MedicoId { get; set; }

        public int PacienteId { get; set; }

        public AgendaStatusEnum Status { get; set; }

        public DateTime DataAgendada { get; set; }
    }
}
