// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoHorario.cs" company="TJ Sistemas">
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
    public class AgendaMedicoHorario : EntityBase
    {
        public int AgendaMedicoDiaId { get; set; }

        public DateTime DataEhHora { get; set; }

        public HorarioDisponivelEnum Disponivel { get; set; }

    }
}
