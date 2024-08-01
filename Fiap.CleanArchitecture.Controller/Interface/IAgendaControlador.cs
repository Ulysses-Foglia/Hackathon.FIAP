// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaControlador.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IAgendaControlador
    {

        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda);

        public string BusqueTodasAgendasDoMedico(int idMedico);

        public string AtualizeAhDisponibilidadeDaAgendaDoMedico(int idAgenda, string disponibilidade);

        public string AtualizeHorarioDaAgenda(AgendaMedicoAtualizeHorarioDAO dados);

        public int AtualizeHorarioDaAgendaComPaciente(AgendaMedicoAgendarPacienteDAO dados);

    }
}
