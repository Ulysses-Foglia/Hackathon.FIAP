using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface ILoginRepository
    {
        string GerarToken(Usuario usuario);
    }
}
