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

        public int AtualizeHorarioDaAgendaComPaciente(int idHorario, int IdAgendaMedico, int IdPaciente, string disponibilidade, byte[] versaoLinha)
        {
            return _database.AtualizeHorarioDaAgendaComPaciente(idHorario, IdAgendaMedico, IdPaciente, disponibilidade, versaoLinha);
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

        public byte[] ObtenhaAhVersaoDaLinhaDoHorario(int idHorario)
        {
            return _database.ObtenhaAhVersaoDaLinhaDoHorario(idHorario);
        }

        public AgendaMedicoDia BusqueAgendaDiaDoMedicoPorId(int idHorario) 
        {
            return _database.BusqueAgendaDiaDoMedicoPorId(idHorario);
        }


        public IEnumerable<AgendaMedicoDia> BusqueTodosHorariosDaAgendaPorId(int IdAgendaMedico) 
        {
            return _database.BusqueTodosHorariosDaAgendaPorId(IdAgendaMedico);
        }

        public int RemovaAgendaEhHorarioDaAgenda(int idAgendaMedico)
        {
            return _database.RemovaAgendaEhHorarioDaAgenda(idAgendaMedico);
        }

        public int RemovaHorarioDaAgenda(int idHorario, int idAgendaMedico)
        {
            return _database.RemovaHorarioDaAgenda(idHorario,idAgendaMedico);
        }

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDosMedicos(int Limite)
        {
            return _database.BusqueTodasAgendasDosMedicos(Limite);
        }
    }
}
