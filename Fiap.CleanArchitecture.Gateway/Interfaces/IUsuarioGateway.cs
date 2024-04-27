using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface IUsuarioGateway
    {
        IEnumerable<Usuario> BuscarTodos();
    }
}
