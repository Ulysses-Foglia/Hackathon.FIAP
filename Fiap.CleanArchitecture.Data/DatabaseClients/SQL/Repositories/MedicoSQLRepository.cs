using Dapper;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts;


namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    public class MedicoSQLRepository : Repository
    {
        private readonly IConfiguration _configuration;

        public MedicoSQLRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public string GerarToken(Medico medico)
        {
            if (!MedicoAutenticado(medico, out Medico medicoAutenticado))
                throw new Exception("Erro ao autenticar o médico, por favor, tente novamente!");

            var expires = int.Parse(_configuration.GetSection("Authentication:ExpireTimeInHour").Value);
            var secret = _configuration.GetSection("Authentication:Secret").Value;
            var key = Encoding.UTF8.GetBytes(secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, medicoAutenticado.Papel.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = Crypto.Encode(medico.Email),
                Issuer = "API-Fiap.CleanArchitecture",
                Subject = new ClaimsIdentity(claims, "Custom"),
                Expires = DateTime.UtcNow.AddHours(expires),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature,
                    SecurityAlgorithms.Sha256Digest)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool MedicoAutenticado(Medico medico, out Medico medicoAutenticado)
        {
            bool autenticado = false;

            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = UsuarioSQLScript.VerificarUsuario;

                var param = new DynamicParameters();

                param.Add("@EMAIL", medico.Email, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@SENHA", medico.Senha, DbType.AnsiString, ParameterDirection.Input, 20);

                medicoAutenticado = conn.QuerySingleOrDefault<Medico>(sql, param);

                if (medicoAutenticado != null)
                    autenticado = true;
            }

            return autenticado;
        }

        public IEnumerable<Medico> BuscarTodos()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = UsuarioSQLScript.BuscarTodos;

                var result = conn.Query<Medico>(sql, commandTimeout: Timeout);

                return result;
            }
        }

        public Medico BuscarPorId(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = UsuarioSQLScript.BuscarPorId;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);

                var result = conn.QueryFirstOrDefault<Medico>(sql, param, commandTimeout: Timeout);

                return result;
            }
        }

        public void Criar(Medico medico)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                var sql = UsuarioSQLScript.Criar;

                comd.CommandText = sql;

                comd.Parameters.AddWithValue("@NOME", medico.Nome);
                comd.Parameters.AddWithValue("@EMAIL", medico.Email);
                comd.Parameters.AddWithValue("@SENHA", medico.Senha);
                comd.Parameters.AddWithValue("@PAPEL", medico.Papel);

                comd.ExecuteScalar();
                trans.Commit();
            }
        }

        public Medico Alterar(Medico medico)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = UsuarioSQLScript.Alterar;

                var param = new DynamicParameters();

                param.Add("@ID", medico.Id, DbType.Int32, ParameterDirection.Input);
                param.Add("@NOME", medico.Nome, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@EMAIL", medico.Email, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@PAPEL", medico.Papel.ToString(), DbType.AnsiString, ParameterDirection.Input, 20);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }

            return BuscarPorId(medico.Id);
        }

        public void Excluir(int id)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = UsuarioSQLScript.Excluir;

                var param = new DynamicParameters();

                param.Add("@ID", id, DbType.Int32, ParameterDirection.Input);

                conn.Execute(sql, param, commandTimeout: Timeout);
            }
        }
    }
}
