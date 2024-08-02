// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedico.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entities;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class AgendaMedicoMes : EntityBase
    {
        public AgendaMedicoMes(){}

        public Medico Medico { get;  set; }
        public int MedicoId { get;  set; }
        public string MesAno { get; set; }
        public int Dia { get; set; }
        public DiaDisponivelEnum DiaDisponivel { get; set; }

        public ICollection<AgendaMedicoDia> DiasDaAgenda { get;  set; }

        public AgendaMedicoMes(AgendaMedicoMesDAO agendaMedicoDAO, bool EhNovoCadastro)
        {
            ValideEntity(agendaMedicoDAO, EhNovoCadastro);

            if (!EhNovoCadastro && !MedicoIdValido(agendaMedicoDAO.MedicoId))
                throw new Exception(MensagensValidacoes.Agenda_Mes_IdMedico);
            if (!MesAnoValido(agendaMedicoDAO.MesAno))
                throw new Exception(MensagensValidacoes.Agenda_Mes_MesAno);
            if(!DiaValido(agendaMedicoDAO.Dia))
                throw new Exception(MensagensValidacoes.Agenda_Mes_Dia);
            if (!DiaDisponivelValido(agendaMedicoDAO.DiaDisponivel))
                throw new Exception(MensagensValidacoes.Agenda_Mes_DiaDisponivel);
            if (!MedicoDiasDaAgenda(agendaMedicoDAO.ConvertaDiasDaAgendaEntity(EhNovoCadastro)))
                throw new Exception(MensagensValidacoes.Agenda_Mes_DiaAgenda);

            this.Medico = new Medico() { Id = agendaMedicoDAO.MedicoId };
            this.MedicoId = agendaMedicoDAO.MedicoId;
            this.MesAno = agendaMedicoDAO.MesAno;
            this.Dia = agendaMedicoDAO.Dia;
            this.DiaDisponivel = Enum.GetValues<DiaDisponivelEnum>().First(x => x.ToString().Equals(agendaMedicoDAO.DiaDisponivel));
            this.DiasDaAgenda = agendaMedicoDAO.ConvertaDiasDaAgendaEntity(EhNovoCadastro);
        }

        public AgendaMedicoMesDAO ConvertaEmDAO()
        {
            var listaNova = new List<AgendaMedicoDiaDAO>();
            foreach (var item in DiasDaAgenda)
            {
                listaNova.Add(item.ConvertaEmDAO());
            }

            return new AgendaMedicoMesDAO()
            {
                Id = this.Id,
                MedicoId = this.MedicoId,
                Dia = this.Dia,
                DiaDisponivel = this.DiaDisponivel.ToString(),
                MesAno = this.MesAno,
                DiasDaAgenda = listaNova,

            };
        }

        #region VALIDAÇÕES

        private bool MedicoIdValido(int Id) => Id != 0;

        private bool MedicoDiasDaAgenda(ICollection<AgendaMedicoDia> agendaMedicoDias) => agendaMedicoDias.Any() && agendaMedicoDias.Count > 0;

        private bool MesAnoValido(string mesAno) => mesAno != null && mesAno.Length > 0 && mesAno.Length <=6;

        private bool DiaValido(int dia) => dia != 0 && dia > 0 && dia <= 31;

        private bool DiaDisponivelValido(string diaDisponivel) => diaDisponivel != null && diaDisponivel.Equals(DiaDisponivelEnum.DISPONIVEL.ToString()) || diaDisponivel.Equals(DiaDisponivelEnum.INDISPONIVEL.ToString());

        #endregion

        private void ValideEntity(AgendaMedicoMesDAO agendaMedicoDAO, bool EhNovoCadastro)
        {
            if (!EhNovoCadastro) { AssertionConcern.AssertArgumentTrue(MedicoIdValido(agendaMedicoDAO.MedicoId), MensagensValidacoes.Agenda_Mes_IdMedico); }
            AssertionConcern.AssertArgumentTrue(MesAnoValido(agendaMedicoDAO.MesAno), MensagensValidacoes.Agenda_Mes_MesAno);
            AssertionConcern.AssertArgumentTrue(DiaValido(agendaMedicoDAO.Dia), MensagensValidacoes.Agenda_Mes_Dia);
            AssertionConcern.AssertArgumentTrue(DiaDisponivelValido(agendaMedicoDAO.DiaDisponivel), MensagensValidacoes.Agenda_Mes_DiaDisponivel);
            AssertionConcern.AssertArgumentTrue(MedicoDiasDaAgenda(agendaMedicoDAO.ConvertaDiasDaAgendaEntity(EhNovoCadastro)), MensagensValidacoes.Agenda_Mes_DiaAgenda);
        }
    }
}
