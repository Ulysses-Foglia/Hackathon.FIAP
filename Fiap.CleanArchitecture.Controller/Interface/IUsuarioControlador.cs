using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IUsuarioControlador
    {
        string GerarToken(UsuarioDAO usuarioDAO);
        string BuscarTodos();
        string BuscarPorId(int id);
        void Criar(UsuarioDAO usuarioDAO);
        string Alterar(UsuarioAlterarDAO usuarioAlterarDAO);
        void Excluir(int id);
    }
}
