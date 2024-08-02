// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoFiltroExclusaoHorarioDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoFiltroExclusaoHorarioDAO
    {
        public AgendaMedicoFiltroExclusaoHorarioDAO()
        {
            
        }
        public int IdHorario { get; set; }

        public int IdAgendaMedico { get; set; }

        public void ValideEntradaDoUsuario()
        {

            if (!IdValido(this.IdHorario))
                throw new Exception("O IdHorario não foi informado.");
            if (!IdValido(this.IdAgendaMedico))
                throw new Exception("O IdAgendaMedico não foi informado.");
          
        }

        private bool IdValido(int Id) => Id != 0;

    }
}
