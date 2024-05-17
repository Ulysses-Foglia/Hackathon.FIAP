using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers.Interfaces
{
    public interface IUsuarioController
    {      
        IActionResult Autenticar([FromBody] UsuarioDAO usuarioDAO);
        IActionResult BuscarTodos();
        IActionResult BuscarPorId(int id);
        IActionResult Criar([FromBody] UsuarioDAO usuarioDAO);
        IActionResult Alterar([FromBody] UsuarioAlterarDAO usuarioAlterarDAO);
        IActionResult Excluir(int id);      
    }
}
