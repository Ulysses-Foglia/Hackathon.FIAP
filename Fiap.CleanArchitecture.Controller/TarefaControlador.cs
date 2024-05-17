using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;
using Fiap.CleanArchitecture.UseCase;
using Fiap.CleanArchitecture.UseCase.Interfaces;

namespace Fiap.CleanArchitecture.Controller
{
    public class TarefaControlador : ITarefaControlador
    {
        private readonly IDatabaseClient _databaseClient;
        private readonly ITarefaGateway _tarefaGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        private readonly ITarefaUseCase _tarefaUseCase;

        public TarefaControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _tarefaGateway = new TarefaGateway(_databaseClient);
            _usuarioGateway = new UsuarioGateway(_databaseClient);
            _tarefaUseCase = new TarefaUseCase(_tarefaGateway, _usuarioGateway);
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
            var tarefa = new Tarefa(_tarefaUseCase.RegistreTarefa(tarefaDAO));

            _tarefaGateway.Criar(tarefa);
        }

        public string Alterar(TarefaAlterarDAO tarefaAlterarDAO)
        {
            var tarefa = new Tarefa(tarefaAlterarDAO);

            var novaTarefa = _tarefaGateway.Alterar(tarefa);

            return TarefaPresenter.ToJson(novaTarefa);
        }

        public string AlterarSituacao(TarefaAlterarSituacaoDAO tarefaAlterarSituacaoDAO)
        {
            if (!Enum.TryParse(tarefaAlterarSituacaoDAO.Status, out ETipoStatus enumTipoStatus))
                throw new Exception("Situação informada não é válida");

            var novaTarefa = _tarefaUseCase.AltereSituacao(tarefaAlterarSituacaoDAO.Id, enumTipoStatus);

            return TarefaPresenter.ToJson(novaTarefa);
        }

        public string AtribuaUmNovoResponsavel(TarefaAtribuirResponsavelDAO tarefaAtribuirResponsavelDAO)
        {
            if (!Enum.TryParse(tarefaAtribuirResponsavelDAO.Status, out ETipoStatus enumTipoStatus))
                throw new Exception("Situação informada não é válida");

            var novaTarefa = _tarefaUseCase
                .AtribuaUmResponsavel(tarefaAtribuirResponsavelDAO.Id, enumTipoStatus, tarefaAtribuirResponsavelDAO.IdResponsavel);

            return TarefaPresenter.ToJson(novaTarefa);
        }

        public string Aprovar(int id)
        {
            var tarefaAprovada = _tarefaUseCase.Aprovar(id);

            return TarefaPresenter.ToJson(tarefaAprovada);
        }

        public void Excluir(int id)
        {
            _tarefaUseCase.ExcluirTarefa(id);
        }
    }
}
