using Fiap.CleanArchitecture.Entity.Attribute;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers.Interfaces
{
    public interface IUsuarioController
    {
      
        public IActionResult Autenticar([FromBody] UsuarioDAO usuarioDAO);

        public IActionResult BuscarTodos();

        public IActionResult BuscarPorId(int id);


        public IActionResult Criar([FromBody] UsuarioDAO usuarioDAO);


        public IActionResult Alterar([FromBody] UsuarioAlterarDAO usuarioAlterarDAO);


        public IActionResult Excluir(int id);
      
    }
}
