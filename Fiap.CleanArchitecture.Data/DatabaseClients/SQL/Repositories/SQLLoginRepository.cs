using Dapper;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    public class SQLLoginRepository : Repository
    {
        private readonly IConfiguration _configuration;

        public SQLLoginRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            bool autenticado = false;

            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = "SELECT 1 FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA";

                var param = new DynamicParameters();

                param.Add("@EMAIL", usuario.Email, DbType.AnsiString, ParameterDirection.Input, 100);
                param.Add("@SENHA", usuario.Senha, DbType.AnsiString, ParameterDirection.Input, 20);
                autenticado = conn.QuerySingle<bool>(sql, param);
            }

            if (!autenticado)
                throw new Exception("Erro ao autenticar o usuário, por favor, tente novamente!");

            var expires = int.Parse(_configuration.GetSection("Authentication:ExpireTimeInHour").Value);
            var secret = _configuration.GetSection("Authentication:Secret").Value;
            var key = Encoding.UTF8.GetBytes(secret);

            var claimsInfo = "buscar infos para repassar no token";

            var claims = new List<Claim>();

            //foreach (var claim in claimsInfo)
            //    claims.Add(new Claim(claimType, claimValue));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = usuario.Email, //criptografado
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
    }
}
