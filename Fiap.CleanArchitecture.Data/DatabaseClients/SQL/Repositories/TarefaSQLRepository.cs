using Dapper;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts;
using Fiap.CleanArchitecture.Entity.DTO;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    public class TarefaSQLRepository : Repository
    {
        public TarefaSQLRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Tarefa> BuscarTodos()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = TarefaSQLScript.BuscarTodos;

                var result = conn.Query<TarefaDTO>(sql, commandTimeout: Timeout);

                return TarefaDTO.ToEntity(result);
            }
        }

        public Tarefa BuscarPorId(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = TarefaSQLScript.BuscarPorId;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);

                var result = conn.QueryFirstOrDefault<TarefaDTO>(sql, param, commandTimeout: Timeout);

                return TarefaDTO.ToEntity(result);
            }
        }

        public void Criar(Tarefa tarefa)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = TarefaSQLScript.Criar;

                var param = new DynamicParameters();
                
                param.Add("@TITULO", tarefa.Titulo, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@DESCRICAO", tarefa.Titulo, DbType.AnsiString, ParameterDirection.Input, 2000);
                param.Add("@PRAZO_VALOR", tarefa.Prazo.Valor, DbType.Int32, ParameterDirection.Input);
                param.Add("@PRAZO_UNIDADE", tarefa.Prazo.Unidade.ToString()[0], DbType.AnsiStringFixedLength, ParameterDirection.Input, 1);
                param.Add("@STATUS", tarefa.Status.ToString(), DbType.AnsiString, ParameterDirection.Input, 20);
                param.Add("@DATA_INICIO", tarefa.DataInicio, DbType.DateTime, ParameterDirection.Input);
                param.Add("@DATA_FIM", tarefa.DataFim, DbType.DateTime, ParameterDirection.Input);
                param.Add("@ID_CRIADOR", tarefa.Criador.Id, DbType.Int32, ParameterDirection.Input);
                param.Add("@ID_RESPONSAVEL", tarefa.Responsavel?.Id, DbType.Int32, ParameterDirection.Input);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }
        }

        public Tarefa Alterar(Tarefa tarefa)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = TarefaSQLScript.Alterar;

                var param = new DynamicParameters();

                param.Add("@ID", tarefa.Id, DbType.Int32, ParameterDirection.Input);
                param.Add("@TITULO", tarefa.Titulo, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@DESCRICAO", tarefa.Titulo, DbType.AnsiString, ParameterDirection.Input, 2000);
                param.Add("@PRAZO_VALOR", tarefa.Prazo.Valor, DbType.Int32, ParameterDirection.Input);
                param.Add("@PRAZO_UNIDADE", tarefa.Prazo.Unidade.ToString()[0], DbType.AnsiStringFixedLength, ParameterDirection.Input, 1);
                param.Add("@STATUS", tarefa.Status.ToString(), DbType.AnsiString, ParameterDirection.Input, 20);
                param.Add("@DATA_INICIO", tarefa.DataInicio, DbType.DateTime, ParameterDirection.Input);
                param.Add("@DATA_FIM", tarefa.DataFim, DbType.DateTime, ParameterDirection.Input);
                param.Add("@ID_RESPONSAVEL", tarefa.Responsavel.Id, DbType.Int32, ParameterDirection.Input);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }

            return BuscarPorId(tarefa.Id);
        }

        public void Aprovar(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = TarefaSQLScript.Aprovar;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);
                param.Add("@STATUS", ETipoStatus.Concluida.ToString(), DbType.AnsiString, ParameterDirection.Input, 20);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }
        }

        public void Excluir(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = TarefaSQLScript.Excluir;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }
        }
    }
}
