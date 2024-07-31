// ------------------------------------------------------------------------------------------------------
// <copyright file="IAgendaUseCase.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface IAgendaUseCase
    {
        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda);
    }
}
