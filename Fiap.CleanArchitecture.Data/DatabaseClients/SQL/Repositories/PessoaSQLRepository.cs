using Dapper;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts;
using Fiap.CleanArchitecture.Entity.DTO;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    public class PessoaSQLRepository : Repository
    {
        public PessoaSQLRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Pessoa> BuscarTodos()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = PessoaSQLScript.BuscarTodos;

                var result = conn.Query<PessoaDTO>(sql, commandTimeout: Timeout);

                return PessoaDTO.ToEntity(result);
            }
        }

        public Pessoa BuscarPorId(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = PessoaSQLScript.BuscarPorId;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);

                var result = conn.QueryFirstOrDefault<PessoaDTO>(sql, param, commandTimeout: Timeout);

                return PessoaDTO.ToEntity(result);
            }
        }

        public void Criar(Pessoa pessoa)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = PessoaSQLScript.Criar;

                var param = new DynamicParameters();

                param.Add("@NOME", pessoa.Nome, DbType.AnsiString, ParameterDirection.Input, 100);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }
        }

        public Pessoa Alterar(Pessoa pessoa)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = PessoaSQLScript.Alterar;

                var param = new DynamicParameters();

                param.Add("@ID", pessoa.Id, DbType.Int32, ParameterDirection.Input);
                param.Add("@NOME", pessoa.Nome, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@ID_USUARIO", pessoa.Usuario.Id, DbType.Int32, ParameterDirection.Input);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }

            return BuscarPorId(pessoa.Id);
        }

        public void Excluir(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = PessoaSQLScript.Excluir;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }
        }
    }
}
