using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Controller
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
