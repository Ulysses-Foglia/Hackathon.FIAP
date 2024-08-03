// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaControlador.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IAgendaControlador
    {
        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda);
        public string BusqueTodasAgendasDoMedico(int idMedico);
        public string AtualizeAhDisponibilidadeDaAgendaDoMedico(int idAgenda, string disponibilidade);
        public string AtualizeHorarioDaAgenda(AgendaMedicoAtualizeHorarioDAO dados);
        public int AtualizeHorarioDaAgendaComPaciente(AgendaMedicoAgendarPacienteDAO dados);
        public int CrieHorarioNaAgendaDoMedico(AgendaMedicoDiaDAO dados);
        public string BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(AgendaMedicoFiltroIdMedicoDiaMesAnoDAO dados);
        public string RemovaAgendaEhHorarioDaAgenda(int idAgendaMedico);
        public string RemovaHorarioDaAgenda(AgendaMedicoFiltroExclusaoHorarioDAO dados);
        public string BusqueTodasAsAgendasDosMedicos(int limiteLinhas);
    }
}
