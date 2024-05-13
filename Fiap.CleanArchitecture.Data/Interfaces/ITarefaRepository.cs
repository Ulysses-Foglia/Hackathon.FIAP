using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface ITarefaRepository
    {
        IEnumerable<Tarefa> BuscarTodasTarefas();
        Tarefa BuscarTarefaPorId(int id);
        void CriarTarefa(Tarefa tarefa);
        Tarefa AlterarTarefa(Tarefa tarefa);
        void Aprovar(int id);
        void ExcluirTarefa(int id);
    }
}
