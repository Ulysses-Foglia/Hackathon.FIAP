// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoDia.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class AgendaMedicoDia : EntityBase
    { 

        public DiaDaSemanaEnum  Dia { get; set; }

        public DiaDisponivelEnum DiaDisponivel { get; set; }

        public ICollection<AgendaMedicoHorario> Horarios { get; set; }

    }
}
