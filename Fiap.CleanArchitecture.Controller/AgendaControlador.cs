// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaControlador.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;
using Fiap.CleanArchitecture.UseCase;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace Fiap.CleanArchitecture.Controller
{
    public class AgendaControlador : IAgendaControlador
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseClient _databaseClient;
        private readonly IAgendaGateway _agendaGateway;
        private readonly IAgendaUseCase _agendaUseCase;
        private readonly IMedicoGateway _medicoGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly IEmailGateway _emailGateway;

        public AgendaControlador(IDatabaseClient databaseClient,  IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseClient = databaseClient;
            _agendaGateway = new AgendaGateway(_databaseClient);
            _medicoGateway = new MedicoGateway(_databaseClient);
            _agendaUseCase = new AgendaUseCase(_agendaGateway, _medicoGateway);
            _emailGateway = new EmailGateway(_configuration);
            _usuarioGateway = new UsuarioGateway(_databaseClient);

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

            if (horarioAtual == null) throw new Exception("Horario não localizado, verifique o Id e tente novamente.");

            if (horarioAtual.HorarioDisponivel == Entity.Enums.HorarioDisponivelEnum.INDISPONIVEL) 
            {
                throw new Exception("O Horário já esta reservado.");
            }
    
            int resposta = _agendaGateway.AtualizeHorarioDaAgendaComPaciente(dados.IdHorario, dados.IdAgendaMedico, dados.IdPaciente, "INDISPONIVEL", versaoLinhaAtual ?? []);

            if (resposta != 0) 
            {
                //envio do email aqui
                try
                {
                    var IdDoMedico = _agendaGateway.BusqueTodasAgendasDosMedicos(20000).Where(x => x.Id == dados.IdAgendaMedico).FirstOrDefault().MedicoId;
                    var Medico = _medicoGateway.BuscarPorId(IdDoMedico);
                    var Paciente = _usuarioGateway.BuscarPorId(dados.IdPaciente);

                    _emailGateway.SendMailMessage(new Entity.DAOs.Email.EmailDAO() { DataHora = DateTime.Now, MedicoEmail = Medico.Email, MedicoNome = Medico.Nome, PacienteNome = Paciente.Nome });
                }
                catch (Exception)
                {
                    throw new Exception("Erro ao disparar mensagem de email para o serviço.");
                }

            }

            return resposta;
        }

        public int CrieHorarioNaAgendaDoMedico(AgendaMedicoDiaDAO dados) 
        {
            var agendaDia = new AgendaMedicoDia(dados, true);

            var agendasDoDia = _agendaGateway.BusqueTodosHorariosDaAgendaPorId(agendaDia.AgendaMedicoId);

            if (agendasDoDia.Any(a => a.Horario.Equals(agendaDia.Horario))) 
            {
                throw new Exception("Já existe um horário criado para esse dia");
            }

            return _agendaGateway.CrieHorarioNaAgendaDoMedico(agendaDia);
        }

        public string BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(AgendaMedicoFiltroIdMedicoDiaMesAnoDAO dados)
        {
            var agendas = _agendaGateway.BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(dados.IdMedico, dados.Dia, dados.MesAno);

            return AgendaPresenter.ToJson(agendas);
        }

        public string RemovaAgendaEhHorarioDaAgenda(int idAgendaMedico) 
        {
            return _agendaGateway.RemovaAgendaEhHorarioDaAgenda(idAgendaMedico) != 0 ? "Agenda e horarios removidos" : "Agenda não removida ou encontrada";
        }

        public string RemovaHorarioDaAgenda(AgendaMedicoFiltroExclusaoHorarioDAO dados)
        {
            return _agendaGateway.RemovaHorarioDaAgenda(dados.IdHorario, dados.IdAgendaMedico) != 0 ? "Horario removido com sucesso" : "Horário não removido ou encontrado";
        }

        public string BusqueTodasAsAgendasDosMedicos(int limiteLinhas) 
        {
            var registros = _agendaGateway.BusqueTodasAgendasDosMedicos(limiteLinhas);

            return AgendaPresenter.ToJson(registros);
        }
    }
}
