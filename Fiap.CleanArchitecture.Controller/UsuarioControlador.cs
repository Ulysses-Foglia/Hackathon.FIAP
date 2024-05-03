using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Attribute;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
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

        public string GerarToken(UsuarioDAO usuarioDAO)
        {
            var usuario = new Usuario(usuarioDAO.Email, usuarioDAO.Senha);

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

        public void Criar(UsuarioDAO usuarioDAO)
        {
            var usuario = new Usuario(usuarioDAO);

            _usuarioGateway.Criar(usuario);
        }

        public string Alterar(UsuarioAlterarDAO usuarioAlterarDAO)
        {
            var usuario = new Usuario(usuarioAlterarDAO);

            var novoUsuario = _usuarioGateway.Alterar(usuario);

            return UsuarioPresenter.ToJson(novoUsuario);
        }

        public void Excluir(int id)
        {
            _usuarioGateway.Excluir(id);
        }
    }
}
