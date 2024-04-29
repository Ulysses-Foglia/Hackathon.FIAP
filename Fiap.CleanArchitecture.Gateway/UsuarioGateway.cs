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

        public string GerarToken(Usuario usuario)
        {
            return _database.GerarToken(usuario);
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return _database.BuscarTodos();
        }

        public Usuario BuscarPorId(int id)
        {
            return _database.BuscarPorId(id);
        }

        public void Criar(Usuario usuario)
        {
            _database.Criar(usuario);
        }

        public Usuario Alterar(Usuario usuario)
        {
            return _database.Alterar(usuario);
        }

        public void Excluir(int id)
        {
            _database.Excluir(id);
        }
    }
}
