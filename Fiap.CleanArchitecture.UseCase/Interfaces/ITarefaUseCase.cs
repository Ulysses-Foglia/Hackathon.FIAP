using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface ITarefaUseCase
    {
        public TarefaDAO RegistreTarefa(TarefaDAO tarefa);

        public Tarefa AltereSituacao(int IdTarefa, ETipoStatus? status);

        public Tarefa AtribuaUmResponsavel(int IdTarefa, ETipoStatus? status, int IdResponsavel);
    }
}
