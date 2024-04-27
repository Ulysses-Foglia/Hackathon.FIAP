using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Gateway
{
    public class UsuarioGateway : IUsuarioGateway
    {
        private readonly IDatabaseClient _database;

        public UsuarioGateway(IDatabaseClient database)
        {
            _database = database;
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return _database.BuscarTodos();
        }
    }
}
