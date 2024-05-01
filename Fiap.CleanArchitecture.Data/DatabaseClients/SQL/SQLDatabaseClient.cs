using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL
{
    public class SQLDatabaseClient : IDatabaseClient
    {
        private readonly IConfiguration _configuration;

        private readonly UsuarioSQLRepository _usuarioSQLRepository;
        private readonly PessoaSQLRepository _pessoaSQLRepository;
        private readonly TarefaSQLRepository _tarefaSQLRepository;

        public SQLDatabaseClient(IConfiguration configuration)
        {
            _configuration = configuration;

            _usuarioSQLRepository = new UsuarioSQLRepository(_configuration);
            _pessoaSQLRepository = new PessoaSQLRepository(_configuration);
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

        #region PessoaRepository
        public IEnumerable<Pessoa> BuscarTodasPessoas() => _pessoaSQLRepository.BuscarTodos();
        public Pessoa BuscarPessoaPorId(int id) => _pessoaSQLRepository.BuscarPorId(id);
        public void CriarPessoa(Pessoa usuario) => _pessoaSQLRepository.Criar(usuario);
        public Pessoa AlterarPessoa(Pessoa usuario) => _pessoaSQLRepository.Alterar(usuario);
        public void ExcluirPessoa(int id) => _pessoaSQLRepository.Excluir(id);
        #endregion

        #region TarefaRepository
        public IEnumerable<Tarefa> BuscarTodasTarefas() => _tarefaSQLRepository.BuscarTodos();
        public Tarefa BuscarTarefaPorId(int id) => _tarefaSQLRepository.BuscarPorId(id);
        public void CriarTarefa(Tarefa tarefa) => _tarefaSQLRepository.Criar(tarefa);
        public Tarefa AlterarTarefa(Tarefa tarefa) => _tarefaSQLRepository.Alterar(tarefa);
        public void ExcluirTarefa(int id) => _tarefaSQLRepository.Excluir(id);
        #endregion
    }
}
