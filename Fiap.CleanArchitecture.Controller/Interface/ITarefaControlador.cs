using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface ITarefaControlador
    {
        string BuscarTodos();
        string BuscarPorId(int id);
        void Criar(TarefaDAO tarefaDAO);
        string Alterar(TarefaAlterarDAO tarefaAlterarDAO);
        void Excluir(int id);
        string Aprovar(int id);
        string AlterarSituacao(TarefaAlterarSituacaoDAO tarefaAlterarSituacaoDAO);
        string AtribuaUmNovoResponsavel(TarefaAtribuirResponsavelDAO tarefaAtribuirResponsavelDAO);
    }
}
