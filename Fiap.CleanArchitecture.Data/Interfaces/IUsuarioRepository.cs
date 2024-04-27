using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> BuscarTodos();
    }
}
