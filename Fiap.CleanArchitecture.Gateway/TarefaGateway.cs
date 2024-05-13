using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Gateway
{
    public class TarefaGateway : ITarefaGateway
    {
        private readonly IDatabaseClient _database;

        public TarefaGateway(IDatabaseClient database)
        {
            _database = database;
        }

        public IEnumerable<Tarefa> BuscarTodos() => _database.BuscarTodasTarefas();
        public Tarefa BuscarPorId(int id) => _database.BuscarTarefaPorId(id);
        public void Criar(Tarefa tarefa) => _database.CriarTarefa(tarefa);
        public Tarefa Alterar(Tarefa tarefa) => _database.AlterarTarefa(tarefa);
        public void Excluir(int id) => _database.ExcluirTarefa(id);

        public void Aprovar(int id) => _database.Aprovar(id);
    }
}
