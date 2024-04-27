using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface ILoginGateway
    {
        string GerarToken(Usuario usuario);
    }
}
