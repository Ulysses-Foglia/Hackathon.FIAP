using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;

namespace Fiap.CleanArchitecture.Controller
{
    public class TarefaControlador : ITarefaControlador
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly ITarefaGateway _tarefaGateway;

        public TarefaControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _tarefaGateway = new TarefaGateway(_databaseClient);
        }

        public string BuscarTodos()
        {
            var tarefas = _tarefaGateway.BuscarTodos();

            return TarefaPresenter.ToJson(tarefas);
        }

        public string BuscarPorId(int id)
        {
            var tarefa = _tarefaGateway.BuscarPorId(id);

            return TarefaPresenter.ToJson(tarefa);
        }

        public void Criar(TarefaDAO tarefaDAO)
        {
            var tarefa = new Tarefa(tarefaDAO);

            _tarefaGateway.Criar(tarefa);
        }

        public string Alterar(TarefaAlterarDAO tarefaAlterarDAO)
        {
            var tarefa = new Tarefa(tarefaAlterarDAO);

            var novaTarefa = _tarefaGateway.Alterar(tarefa);

            return TarefaPresenter.ToJson(novaTarefa);
        }

        public void Excluir(int id)
        {
            _tarefaGateway.Excluir(id);
        }
    }
}
