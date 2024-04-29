using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL
{
    public class SQLDatabaseClient : IDatabaseClient
    {
        private readonly IConfiguration _configuration;
        //private readonly SQLLoginRepository _sqlLoginRepository;
        private readonly UsuarioSQLRepository _usuarioSQLRepository;

        public SQLDatabaseClient(IConfiguration configuration)
        {
            _configuration = configuration;
            //_sqlLoginRepository = new SQLLoginRepository(_configuration);
            _usuarioSQLRepository = new UsuarioSQLRepository(_configuration);
        }

        //#region LoginRepository
        //#endregion

        #region UsuarioRepository
        public string GerarToken(Usuario usuario) => _usuarioSQLRepository.GerarToken(usuario);
        public IEnumerable<Usuario> BuscarTodos() => _usuarioSQLRepository.BuscarTodos();
        public Usuario BuscarPorId(int id) => _usuarioSQLRepository.BuscarPorId(id);
        public void Criar(Usuario usuario) => _usuarioSQLRepository.Criar(usuario);
        public Usuario Alterar(Usuario usuario) => _usuarioSQLRepository.Alterar(usuario);
        public void Excluir(int id) => _usuarioSQLRepository.Excluir(id);
        #endregion
    }
}
