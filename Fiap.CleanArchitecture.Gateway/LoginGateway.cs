using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Gateway
{
    public class LoginGateway : ILoginGateway
    {
        private readonly IDatabaseClient _database;

        public LoginGateway(IDatabaseClient database)
        {
            _database = database;
        }

        public string GerarToken(Usuario usuario)
        {
            return _database.GerarToken(usuario);
        }
    }
}
