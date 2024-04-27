using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;

namespace Fiap.CleanArchitecture.Controller
{
    public class UsuarioControlador
    {
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly IDatabaseClient _databaseClient;

        public UsuarioControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _usuarioGateway = new UsuarioGateway(_databaseClient);
        }

        public string BuscarTodosUsuarios()
        {
            var usuarios = _usuarioGateway.BuscarTodos();

            return UsuarioPresenter.ToJson(usuarios);
        }
    }
}
