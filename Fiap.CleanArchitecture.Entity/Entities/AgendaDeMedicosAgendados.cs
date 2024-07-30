// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaDeMedicosAgendados.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------



using Fiap.CleanArchitecture.Entity.Enums;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class AgendaDeMedicosAgendados : EntityBase
    {
        public int MedicoId { get; private set; }

        public Medico Medico { get; private set; }

        public int PacienteId { get; private set; }

        public Usuario Paciente { get; private set; }

        public AgendaStatusEnum  Status { get; private set; }

        public DateTime DataAgendada { get; private set; }


    }
}
