using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface ITarefaControlador
    {
        public string BuscarTodos();
        public string BuscarPorId(int id);
        public void Criar(TarefaDAO tarefaDAO);
        public string Alterar(TarefaAlterarDAO tarefaAlterarDAO);
        public void Excluir(int id);
        public string AlterarSituacao(int IdTarefa, string situacao);
        public string AtribuaUmNovoResponsavel(int IdTarefa, string situacao, int IdResponsavel);
    }
}
