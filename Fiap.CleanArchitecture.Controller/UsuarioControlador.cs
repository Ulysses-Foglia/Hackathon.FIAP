using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;
using Fiap.CleanArchitecture.UseCase;
using Fiap.CleanArchitecture.UseCase.Interfaces;

namespace Fiap.CleanArchitecture.Controller
{
    public class UsuarioControlador : IUsuarioControlador
    {
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly ITarefaGateway _tarefaGateway;
        private readonly IDatabaseClient _databaseClient;
        private readonly IUsuarioUseCase _usuarioUserCase;

        public UsuarioControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _usuarioGateway = new UsuarioGateway(_databaseClient);           
            _tarefaGateway = new TarefaGateway(_databaseClient);
            _usuarioUserCase = new UsuarioUseCase(_usuarioGateway, _tarefaGateway);
        }

        public string GerarToken(UsuarioDAO usuarioDAO)
        {
            return _usuarioUserCase.AutentiqueUsuario(usuarioDAO);
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
            _usuarioUserCase.CadastreNovoUsuario(usuarioDAO);
        }

        public string Alterar(UsuarioAlterarDAO usuarioAlterarDAO)
        {
            var novoUsuario = _usuarioUserCase.AltereUsuaio(usuarioAlterarDAO);

            return UsuarioPresenter.ToJson(novoUsuario);
        }

        public void Excluir(int id)
        {
            _usuarioUserCase.ExcluaUsuario(id);
        }

     
    }
}
