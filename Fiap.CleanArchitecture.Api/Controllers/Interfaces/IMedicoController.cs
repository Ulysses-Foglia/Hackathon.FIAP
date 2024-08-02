using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers.Interfaces
{
    public interface IMedicoController
    {
        IActionResult AutenticarMedico([FromBody] AutenticacaoModelDAO dados);
        IActionResult BuscarTodos();
        IActionResult BuscarMedicosDisponibilidade();
        IActionResult BuscarPorId(int id);
        IActionResult Criar([FromBody] MedicoDAO medicoDAO);
        IActionResult Alterar([FromBody] MedicoAlterarDAO medicoAlterarDAO);
        IActionResult Excluir(int id);
    }
}
