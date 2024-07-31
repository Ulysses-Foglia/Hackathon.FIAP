// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaGateway.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;


namespace Fiap.CleanArchitecture.Gateway
{
    public class AgendaGateway : IAgendaGateway
    {
        private readonly IDatabaseClient _database;
        public AgendaGateway(IDatabaseClient databaseClient)
        {
            _database = databaseClient;
        }

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico)
        {
            return _database.BusqueTodasAgendasDoMedico(idMedico);
        }

        public int CrieAgendaDoMedico(AgendaMedicoMes agenda)
        {
            return _database.CrieAgendaDoMedico(agenda);
        }
    }
}
