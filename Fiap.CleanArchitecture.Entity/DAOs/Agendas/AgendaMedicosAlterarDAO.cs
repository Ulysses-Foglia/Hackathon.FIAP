// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicosAlterarDAO.cs" company="TJ Sistemas">
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
    public class AgendaMedicosAlterarDAO
    {
        public Usuario Medico { get; private set; }

        public int MedicoId { get; private set; }

        public ICollection<AgendaMedicoDia> DiasDaAgenda { get; private set; }
    }
}
