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
        private readonly TarefaSQLRepository _tarefaSQLRepository;
        private readonly AgendaSQLRepository _agentaSQLRepository;


        public SQLDatabaseClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _agentaSQLRepository = new AgendaSQLRepository(configuration);
            _usuarioSQLRepository = new UsuarioSQLRepository(_configuration);
            _tarefaSQLRepository = new TarefaSQLRepository(_configuration);
        }

        #region UsuarioRepository
        public string GerarToken(Usuario usuario) => _usuarioSQLRepository.GerarToken(usuario);
        public IEnumerable<Usuario> BuscarTodosUsuarios() => _usuarioSQLRepository.BuscarTodos();
        public Usuario BuscarUsuarioPorId(int id) => _usuarioSQLRepository.BuscarPorId(id);
        public void CriarUsuario(Usuario usuario) => _usuarioSQLRepository.Criar(usuario);
        public Usuario AlterarUsuario(Usuario usuario) => _usuarioSQLRepository.Alterar(usuario);
        public void ExcluirUsuario(int id) => _usuarioSQLRepository.Excluir(id);
        #endregion

        #region TarefaRepository
        public IEnumerable<Tarefa> BuscarTodasTarefas() => _tarefaSQLRepository.BuscarTodos();
        public Tarefa BuscarTarefaPorId(int id) => _tarefaSQLRepository.BuscarPorId(id);
        public void CriarTarefa(Tarefa tarefa) => _tarefaSQLRepository.Criar(tarefa);
        public Tarefa AlterarTarefa(Tarefa tarefa) => _tarefaSQLRepository.Alterar(tarefa);
        public void ExcluirTarefa(int id) => _tarefaSQLRepository.Excluir(id);

        public void Aprovar(int id) => _tarefaSQLRepository.Aprovar(id);
        #endregion

        #region AgendaRepository

        public int CrieAgendaDoMedico(AgendaMedicoMes agenda) => _agentaSQLRepository.CrieAgendaDoMedico(agenda);

        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico) => _agentaSQLRepository.BusqueTodasAgendasDoMedico(idMedico);


        #endregion
    }
}
