using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.CleanArchitecture.Api.Controllers.Interfaces
{
    public interface IAgendaController
    {
        IActionResult CriarAgendaMedico([FromBody] AgendaMedicoMesDAO angendaDAO);
        IActionResult CriarHorarioAgendaMedico([FromBody] AgendaMedicoDiaDAO agendaMedicoDiaDAO);
        IActionResult AtualizeDiponibilidadeAgenda([FromBody] AgendaMedicoAtualizarDisponibilidadeDAO dados);
        IActionResult AtualizeHorarioAgenda([FromBody] AgendaMedicoAtualizeHorarioDAO dados);
        IActionResult AgendePaciente([FromBody] AgendaMedicoAgendarPacienteDAO dados);
        IActionResult BusqueAgendaPeloFiltro([FromBody] AgendaMedicoFiltroIdMedicoDiaMesAnoDAO dados);
        IActionResult BuscarTodasAgendasPorMedicoId(int id);
        IActionResult BuscarTodasAgendasDosMedicosLimitado([FromQuery] int limite);
        IActionResult DeleteAgendaPeloId(int id);
        IActionResult DeleteHorarioDaAgenda([FromBody] AgendaMedicoFiltroExclusaoHorarioDAO dados);

    }
}
