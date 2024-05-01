using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;

namespace Fiap.CleanArchitecture.Controller
{
    public class TarefaControlador
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly ITarefaGateway _tarefaGateway;
        private readonly IPessoaGateway _pessoaGateway;

        public TarefaControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _tarefaGateway = new TarefaGateway(_databaseClient);
            _pessoaGateway = new PessoaGateway(_databaseClient);
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
            var pessoa = _pessoaGateway.BuscarPorId(tarefaDAO.CriadorId);

            var tarefa = new Tarefa(tarefaDAO, pessoa);

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
