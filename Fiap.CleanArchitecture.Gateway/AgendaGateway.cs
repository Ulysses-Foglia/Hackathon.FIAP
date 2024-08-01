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

        public AgendaMedicoMes AtualizeDisponibilidadeAgendaMedicoPorId(int IdAgenda, string disponibilidade)
        {
            return _database.AtualizeDisponibilidadeAgendaMedicoPorId(IdAgenda, disponibilidade);
        }

        public int AtualizeHorarioDaAgenda(int idHorario, int IdAgendaMedico, string horario)
        {
            return _database.AtualizeHorarioDaAgenda(idHorario, IdAgendaMedico, horario);   
        }

        public int AtualizeHorarioDaAgendaComPaciente(int idHorario, int IdAgendaMedico, int IdPaciente, string disponibilidade)
        {
            return _database.AtualizeHorarioDaAgendaComPaciente(idHorario, IdAgendaMedico, IdPaciente, disponibilidade);
        }

        public int AtualizeLibereHorarioDaAgenda(int idHorario, int IdAgendaMedico)
        {
            return _database.AtualizeLibereHorarioDaAgenda(idHorario, IdAgendaMedico);
        }

        public AgendaMedicoMes BusqueAgendaDoMedicoPorId(int idMedico, int IdAgenda)
        {
            return _database.BusqueAgendaDoMedicoPorId(idMedico, IdAgenda);
        }

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico)
        {
            return _database.BusqueTodasAgendasDoMedico(idMedico);
        }

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(int idMedico, int dia, string mesano)
        {
            return _database.BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(idMedico, dia, mesano);  
        }

        public int CrieAgendaDoMedico(AgendaMedicoMes agenda)
        {
            return _database.CrieAgendaDoMedico(agenda);
        }

        public int CrieHorarioNaAgendaDoMedico(AgendaMedicoDia horario)
        {
            return _database.CrieHorarioNaAgendaDoMedico(horario);
        }
    }
}
