// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoAtualizeHorarioDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoAtualizeHorarioDAO
    {
        public AgendaMedicoAtualizeHorarioDAO()
        {
            
        }

        public int idHorario { get; set; }

        public int idAgendaMedico { get; set; }

        public string Horario { get; set; }

        public void ValideEntradaDoUsuario() 
        {
            if (!HorarioValido(this.Horario))
                throw new Exception(MensagensValidacoes.Agenda_Dia_Horario);
            if (!IdValido(this.idHorario))
                throw new Exception("O Id do horário não foi informado.");
            if (!IdValido(this.idAgendaMedico))
                throw new Exception("O Id da Agenda no Medico não foi informado.");
        }

        private bool IdValido(int Id) => Id != 0;

        private bool HorarioValido(string horario) => horario != null && horario.Count() == 5 && horario.Contains(":") && VerificaStringDaHora(horario);

        private bool VerificaStringDaHora(string hora)
        {
            var rgx = new Regex("^((?!0+:00)\\d{1,}:[0-5][0-9])$");
            return rgx.Match(hora).Success;

        }
    }
}
