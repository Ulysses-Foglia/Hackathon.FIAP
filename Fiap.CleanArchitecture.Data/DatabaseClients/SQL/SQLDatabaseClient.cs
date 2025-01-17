﻿using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL
{
    public class SQLDatabaseClient : IDatabaseClient
    {
        private readonly IConfiguration _configuration;

        private readonly UsuarioSQLRepository _usuarioSQLRepository;
        private readonly MedicoSQLRepository _medicoSQLRepository;
        private readonly AgendaSQLRepository _agentaSQLRepository;


        public SQLDatabaseClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _agentaSQLRepository = new AgendaSQLRepository(configuration);
            _usuarioSQLRepository = new UsuarioSQLRepository(_configuration);
            _medicoSQLRepository = new MedicoSQLRepository(_configuration);
        } 

        public string GerarToken(Medico medico) => _medicoSQLRepository.GerarToken(medico);

        #region MedicoRepository
        public IEnumerable<Medico> BuscarTodosMedicos() => _medicoSQLRepository.BuscarTodos();
        public IEnumerable<Medico> BuscarMedicosDisponibilidade() => _medicoSQLRepository.BuscarMedicosDisponibilidade();
        public Medico BuscarMedicoPorId(int id) => _medicoSQLRepository.BuscarPorId(id);
        public void CriarMedico(Medico medico) => _medicoSQLRepository.Criar(medico);
        public Medico AlterarMedico(Medico medico) => _medicoSQLRepository.Alterar(medico);
        public void ExcluirMedico(int id) => _medicoSQLRepository.Excluir(id);

        #endregion

        #region UsuarioRepository
        public string GerarToken(Usuario usuario) => _usuarioSQLRepository.GerarToken(usuario);
        public IEnumerable<Usuario> BuscarTodosUsuarios() => _usuarioSQLRepository.BuscarTodos();
        public Usuario BuscarUsuarioPorId(int id) => _usuarioSQLRepository.BuscarPorId(id);
        public void CriarUsuario(Usuario usuario) => _usuarioSQLRepository.Criar(usuario);
        public Usuario AlterarUsuario(Usuario usuario) => _usuarioSQLRepository.Alterar(usuario);
        public void ExcluirUsuario(int id) => _usuarioSQLRepository.Excluir(id);
        #endregion

        #region AgendaRepository
        public int CrieAgendaDoMedico(AgendaMedicoMes agenda) => _agentaSQLRepository.CrieAgendaDoMedico(agenda);
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico) => _agentaSQLRepository.BusqueTodasAgendasDoMedico(idMedico);
        public int CrieHorarioNaAgendaDoMedico(AgendaMedicoDia horario) => _agentaSQLRepository.CrieHorarioNaAgendaDoMedico(horario);
        public AgendaMedicoMes BusqueAgendaDoMedicoPorId(int idMedico, int IdAgenda) => _agentaSQLRepository.BusqueAgendaDoMedicoPorId(idMedico,IdAgenda);
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(int idMedico, int dia, string mesano) => _agentaSQLRepository.BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(idMedico, dia, mesano);
        public AgendaMedicoMes AtualizeDisponibilidadeAgendaMedicoPorId(int IdAgenda, string disponibilidade) => _agentaSQLRepository.AtualizeDisponibilidadeAgendaMedicoPorId(IdAgenda, disponibilidade);
        public int AtualizeHorarioDaAgendaComPaciente(int idHorario, int IdAgendaMedico, int IdPaciente, string disponibilidade, byte[] versaoLinha) => _agentaSQLRepository.AtualizeHorarioDaAgendaComPaciente(idHorario,IdAgendaMedico,IdPaciente,disponibilidade, versaoLinha);
        public int AtualizeLibereHorarioDaAgenda(int idHorario, int IdAgendaMedico) => _agentaSQLRepository.AtualizeLibereHorarioDaAgenda(idHorario,IdAgendaMedico);
        public int AtualizeHorarioDaAgenda(int idHorario, int IdAgendaMedico, string horario) => _agentaSQLRepository.AtualizeHorarioDaAgenda(idHorario, IdAgendaMedico, horario);
        public byte[] ObtenhaAhVersaoDaLinhaDoHorario(int idHorario) => _agentaSQLRepository.ObtenhaAhVersaoDaLinhaDoHorario(idHorario);
        public AgendaMedicoDia BusqueAgendaDiaDoMedicoPorId(int idHorario) => _agentaSQLRepository.BusqueAgendaDiaDoMedicoPorId(idHorario);
        public IEnumerable<AgendaMedicoDia> BusqueTodosHorariosDaAgendaPorId(int IdAgendaMedico) => _agentaSQLRepository.BusqueTodosHorariosDaAgendaPorId(IdAgendaMedico);
        public int RemovaAgendaEhHorarioDaAgenda(int idAgendaMedico) => _agentaSQLRepository.RemovaAgendaEhHorarioDaAgenda(idAgendaMedico);
        public int RemovaHorarioDaAgenda(int idHorario, int idAgendaMedico) => _agentaSQLRepository.RemovaHorarioDaAgenda(idHorario, idAgendaMedico);
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDosMedicos(int Limite) => _agentaSQLRepository.BusqueTodasAgendasDosMedicos(Limite);
        #endregion
    }
}
