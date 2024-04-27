using Dapper;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    public class SQLUsuarioRepository : Repository
    {
        public SQLUsuarioRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = SQLUsuarioScript.BuscarTodos;

                var result = conn.Query<Usuario>(sql, commandTimeout: Timeout);

                return result;
            }
        }
    }
}
