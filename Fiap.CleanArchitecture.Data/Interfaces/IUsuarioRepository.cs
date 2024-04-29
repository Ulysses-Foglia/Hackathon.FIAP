using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        string GerarToken(Usuario usuario);
        IEnumerable<Usuario> BuscarTodos();
        Usuario BuscarPorId(int id);
        void Criar(Usuario usuario);
        Usuario Alterar(Usuario usuario);
        void Excluir(int id);
    }
}
