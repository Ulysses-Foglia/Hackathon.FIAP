// ------------------------------------------------------------------------------------------------------
// <copyright file="IAgendaUseCase.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface IAgendaUseCase
    {
        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda);
    }
}
