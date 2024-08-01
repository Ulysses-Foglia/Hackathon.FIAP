// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoDia.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entities;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class AgendaMedicoDia : EntityBase
    {
        public AgendaMedicoDia(){}
        public int AgendaMedicoId { get; set; }

        public HorarioDisponivelEnum HorarioDisponivel { get; set; }

        public string Horario { get; set; }

        public Usuario Paciente { get; set; }

        public int PacienteId { get; set; }

        public int VersaoLinha { get; set; }

        public AgendaMedicoDia(int agendaMedicoId, HorarioDisponivelEnum horarioDisponivel, string horario, Usuario paciente)
        {
            AgendaMedicoId = agendaMedicoId;
            Horario = horario;
            Paciente = paciente;
            HorarioDisponivel = horarioDisponivel;
        }

        public AgendaMedicoDia(AgendaMedicoDiaDAO agendaMedicoDia, bool EhNovoCadastro)
        {
            ValideEntity(agendaMedicoDia, EhNovoCadastro);

            if (!EhNovoCadastro && !AgendaMedicoIdValido(agendaMedicoDia.AgendaMedicoId))
                throw new Exception(MensagensValidacoes.Agenda_Dia_IdAgenda);
            if (!HorarioDisponivelValido(agendaMedicoDia.HorarioDisponivel))
                throw new Exception(MensagensValidacoes.Agenda_Dia_HoraDisponivel);
            if (!HorarioValido(agendaMedicoDia.Horario))
                throw new Exception(MensagensValidacoes.Agenda_Dia_Horario);


            Id = agendaMedicoDia.Id;
            AgendaMedicoId = agendaMedicoDia.AgendaMedicoId;
            HorarioDisponivel = Enum.GetValues<HorarioDisponivelEnum>().First(e => e.ToString().Equals(agendaMedicoDia.HorarioDisponivel));
            Horario = agendaMedicoDia.Horario;
            PacienteId = agendaMedicoDia.PacienteId;
            VersaoLinha = agendaMedicoDia.VersaoLinha;
        }


        public AgendaMedicoDiaDAO ConvertaEmDAO() 
        {
            return new AgendaMedicoDiaDAO()
            {
                Id = this.Id,
                AgendaMedicoId = this.AgendaMedicoId,
                Horario = this.Horario,
                HorarioDisponivel = this.HorarioDisponivel.ToString(),
                PacienteId = this.PacienteId,
                VersaoLinha = this.VersaoLinha
            };
        }

        #region VALIDACOES

        private bool AgendaMedicoIdValido(int IdAgendaMedico) => IdAgendaMedico != 0;

        private bool HorarioDisponivelValido(string horarioDisponivel) => horarioDisponivel != null && horarioDisponivel.Equals(HorarioDisponivelEnum.INDISPONIVEL.ToString()) 
            || horarioDisponivel.Equals(HorarioDisponivelEnum.DISPONIVEL.ToString());

        private bool HorarioValido(string horario) => horario != null && horario.Count() == 5 && horario.Contains(":") && VerificaStringDaHora(horario);

        private bool VerificaStringDaHora(string hora)
        {
            var rgx = new Regex("^((?!0+:00)\\d{1,}:[0-5][0-9])$");
            return rgx.Match(hora).Success;

        }

        #endregion

        #region TESTES

        private void ValideEntity(AgendaMedicoDiaDAO agendaMedicoDia, bool EhNovoCadastro)
        {
            if (!EhNovoCadastro) { AssertionConcern.AssertArgumentTrue(AgendaMedicoIdValido(agendaMedicoDia.AgendaMedicoId), MensagensValidacoes.Agenda_Dia_IdAgenda); }
            AssertionConcern.AssertArgumentTrue(HorarioDisponivelValido(agendaMedicoDia.HorarioDisponivel), MensagensValidacoes.Agenda_Dia_HoraDisponivel);
            AssertionConcern.AssertArgumentTrue(HorarioValido(agendaMedicoDia.Horario), MensagensValidacoes.Agenda_Dia_Horario);

        }

        #endregion




    }
}
