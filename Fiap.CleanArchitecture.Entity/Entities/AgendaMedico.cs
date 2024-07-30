// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedico.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------


using Fiap.CleanArchitecture.Entity.DAOs.Agendas;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class AgendaMedico : EntityBase
    {
        public Usuario Medico { get; private set; }

        public int MedicoId { get; private set; }

        public ICollection<AgendaMedicoDia> DiasDaAgenda { get; private set; }

        public AgendaMedico(AgendaMedicoDAO agendaMedicoDAO)
        {

            

        }

    }
}
