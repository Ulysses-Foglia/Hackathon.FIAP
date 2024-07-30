// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoAgendadosDAO.cs" company="TJ Sistemas">
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

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaDeMedicosAgendadosDAO
    {
        public int MedicoId { get; set; }

        public Medico Medico { get; set; }

        public int PacienteId { get; set; }

        public Usuario Paciente { get; set; }

        public DateTime DataAgendada { get; set; }
    }
}
