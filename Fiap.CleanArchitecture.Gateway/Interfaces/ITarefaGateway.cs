using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface ITarefaGateway
    {
        IEnumerable<Tarefa> BuscarTodos();
        Tarefa BuscarPorId(int id);
        void Criar(Tarefa tarefa);
        Tarefa Alterar(Tarefa tarefa);
        void Excluir(int id);
    }
}
