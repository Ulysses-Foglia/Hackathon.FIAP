using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface ITarefaUseCase
    {
        TarefaDAO RegistreTarefa(TarefaDAO tarefa);
        Tarefa AltereSituacao(int IdTarefa, ETipoStatus? status);
        Tarefa AtribuaUmResponsavel(int IdTarefa, ETipoStatus? status, int IdResponsavel);
        Tarefa Aprovar(int id);
        void ExcluirTarefa(int id);
    }
}
