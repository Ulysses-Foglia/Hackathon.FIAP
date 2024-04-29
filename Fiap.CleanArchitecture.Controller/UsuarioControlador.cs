using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
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

        public string GerarToken(Usuario usuario)
        {
            var token = _usuarioGateway.GerarToken(usuario);

            return token;
        }

        public string BuscarTodos()
        {
            var usuarios = _usuarioGateway.BuscarTodos();

            return UsuarioPresenter.ToJson(usuarios);
        }

        public string BuscarPorId(int id)
        {
            var usuario = _usuarioGateway.BuscarPorId(id);

            return UsuarioPresenter.ToJson(usuario);
        }

        public void Criar(Usuario usuario)
        {
            _usuarioGateway.Criar(usuario);
        }

        public string Alterar(Usuario usuario)
        {
            var novoUsuario = _usuarioGateway.Alterar(usuario);

            return UsuarioPresenter.ToJson(novoUsuario);
        }

        public void Excluir(int id)
        {
            _usuarioGateway.Excluir(id);
        }
    }
}
