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

        public string GerarToken(Usuario usuario) => _database.GerarToken(usuario);
        public string GerarToken(Medico medico) => _database.GerarToken(medico);
        public IEnumerable<Usuario> BuscarTodos() => _database.BuscarTodosUsuarios();
        public Usuario BuscarPorId(int id) => _database.BuscarUsuarioPorId(id);
        public void Criar(Usuario usuario) => _database.CriarUsuario(usuario);
        public Usuario Alterar(Usuario usuario) => _database.AlterarUsuario(usuario);
        public void Excluir(int id) => _database.ExcluirUsuario(id);
    }
}
