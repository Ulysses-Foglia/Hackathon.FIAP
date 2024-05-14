using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface IUsuarioUseCase
    {
        public void CadastreNovoUsuario(UsuarioDAO usuario);

        public Usuario AltereUsuaio(UsuarioAlterarDAO usuario);

        public string AutentiqueUsuario(UsuarioDAO usuario);

        public void ExcluaUsuario(int IdUsuario);

    }
}
