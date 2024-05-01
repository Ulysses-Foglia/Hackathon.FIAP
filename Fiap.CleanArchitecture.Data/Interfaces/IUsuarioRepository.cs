using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        string GerarToken(Usuario usuario);
        IEnumerable<Usuario> BuscarTodosUsuarios();
        Usuario BuscarUsuarioPorId(int id);
        void CriarUsuario(Usuario usuario);
        Usuario AlterarUsuario(Usuario usuario);
        void ExcluirUsuario(int id);
    }
}
