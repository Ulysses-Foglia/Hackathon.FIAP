using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface ITarefaUseCase
    {
        TarefaDAO RegistreTarefa(TarefaDAO tarefa);
        Tarefa AltereSituacao(int IdTarefa, string situacao);
        Tarefa AtribuaUmResponsavel(int idTarefa, string situacao, int idResponsavel);
        Tarefa Aprovar(int id);
    }
}
