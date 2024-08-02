// ------------------------------------------------------------------------------------------------------
// <copyright file="IAgendaRepository.cs" company="TJ Sistemas">
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

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IAgendaRepository
    {
        public int CrieAgendaDoMedico(AgendaMedicoMes agenda);

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico);

        public int CrieHorarioNaAgendaDoMedico(AgendaMedicoDia horario);

        public AgendaMedicoMes BusqueAgendaDoMedicoPorId(int idMedico, int IdAgenda);

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(int idMedico, int dia, string mesano);

        public AgendaMedicoMes AtualizeDisponibilidadeAgendaMedicoPorId(int IdAgenda, string disponibilidade);

        public int AtualizeHorarioDaAgendaComPaciente(int idHorario, int IdAgendaMedico, int IdPaciente, string disponibilidade, byte[] versaoLinha);

        public int AtualizeLibereHorarioDaAgenda(int idHorario, int IdAgendaMedico);

        public int AtualizeHorarioDaAgenda(int idHorario, int IdAgendaMedico, string horario);

        public byte[] ObtenhaAhVersaoDaLinhaDoHorario(int idHorario);

        public AgendaMedicoDia BusqueAgendaDiaDoMedicoPorId(int idHorario);

        public IEnumerable<AgendaMedicoDia> BusqueTodosHorariosDaAgendaPorId(int IdAgendaMedico);

        public int RemovaAgendaEhHorarioDaAgenda(int idAgendaMedico);

        public int RemovaHorarioDaAgenda(int idHorario, int idAgendaMedico);
    }
}
