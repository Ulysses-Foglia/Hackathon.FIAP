using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL
{
    public class SQLDatabaseClient : IDatabaseClient
    {
        private readonly IConfiguration _configuration;
        private readonly SQLLoginRepository _sqlLoginRepository;
        private readonly SQLUsuarioRepository _sqlUsuarioRepository;

        public SQLDatabaseClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlLoginRepository = new SQLLoginRepository(_configuration);
            _sqlUsuarioRepository = new SQLUsuarioRepository(_configuration);
        }

        #region LoginRepository
        public string GerarToken(Usuario usuario) => _sqlLoginRepository.GerarToken(usuario);
        #endregion

        #region UsuarioRepository
        public IEnumerable<Usuario> BuscarTodos() => _sqlUsuarioRepository.BuscarTodos();
        #endregion
    }
}
