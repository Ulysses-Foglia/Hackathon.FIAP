// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaControlador.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;
using Fiap.CleanArchitecture.UseCase;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Controller
{
    public class AgendaControlador : IAgendaControlador
    {

        private readonly IDatabaseClient _databaseClient;
        private readonly IAgendaGateway _agendaGateway;
        private readonly IAgendaUseCase _agendaUseCase;
        private readonly IMedicoGateway _medicoGateway;

        public AgendaControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _agendaGateway = new AgendaGateway(_databaseClient);
            _medicoGateway = new MedicoGateway(_databaseClient);
            _agendaUseCase = new AgendaUseCase(_agendaGateway, _medicoGateway);
        }

        public string BusqueTodasAgendasDoMedico(int idMedico)
        {
            var agendas = _agendaGateway.BusqueTodasAgendasDoMedico(idMedico);

            return AgendaPresenter.ToJson(agendas);
        }

        public int CrieAgendaDoMedico(AgendaMedicoMesDAO agenda)
        {
            return _agendaUseCase.CrieAgendaDoMedico(agenda);
        }

        public string AtualizeAhDisponibilidadeDaAgendaDoMedico(int idAgenda, string disponibilidade)
        {
            var agenda = _agendaGateway.AtualizeDisponibilidadeAgendaMedicoPorId(idAgenda, disponibilidade);

            return AgendaPresenter.ToJson(agenda);
        }

        public string AtualizeHorarioDaAgenda(AgendaMedicoAtualizeHorarioDAO dados) 
        {

            var linhasAfetadas =  _agendaGateway.AtualizeHorarioDaAgenda(dados.idHorario, dados.idAgendaMedico, dados.Horario);
            if (linhasAfetadas != 0) 
            {
                return AgendaPresenter.ToJson(new { Mensagem = $"O Horario de ID: {dados.idHorario} foi atualizado para {dados.Horario}" });
            }
            else
            {
                return AgendaPresenter.ToJson(new { Mensagem = $"Não foi possível atualizar o horário, verifique os dados e tente novamente." });
            }
        }

        public int AtualizeHorarioDaAgendaComPaciente(AgendaMedicoAgendarPacienteDAO dados)
        {
            //Obtem a versão atual antes de passar para atualizar
            var versaoLinhaAtual = _agendaGateway.ObtenhaAhVersaoDaLinhaDoHorario(dados.IdHorario);
            var horarioAtual = _agendaGateway.BusqueAgendaDiaDoMedicoPorId(dados.IdHorario);

            if (horarioAtual.HorarioDisponivel == Entity.Enums.HorarioDisponivelEnum.INDISPONIVEL) 
            {
                throw new Exception("O Horário já esta reservado.");
            }


           return  _agendaGateway.AtualizeHorarioDaAgendaComPaciente(dados.IdHorario, dados.IdAgendaMedico, dados.IdPaciente, "INDISPONIVEL", versaoLinhaAtual ?? []);

        }

    }
}
