using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface IUsuarioUseCase
    {
        void CadastreNovoUsuario(UsuarioDAO usuario);
        Usuario AltereUsuaio(UsuarioAlterarDAO usuario);
        string AutentiqueUsuario(UsuarioDAO usuario);
        void ExcluaUsuario(int idUsuario);
    }
}
