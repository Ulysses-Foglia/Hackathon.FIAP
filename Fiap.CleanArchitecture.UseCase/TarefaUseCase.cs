using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.DTO;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.UseCase.Interfaces;

namespace Fiap.CleanArchitecture.UseCase
{
    public class TarefaUseCase : ITarefaUseCase
    {

        private readonly ITarefaGateway _tarefaGateway;
        private readonly IUsuarioGateway _usuarioGateway;
        public TarefaUseCase(ITarefaGateway gatewayTarefa, IUsuarioGateway usuarioGateway)
        {
            this._tarefaGateway = gatewayTarefa;
            this._usuarioGateway = usuarioGateway;
        }

        public Tarefa AltereSituacao(int IdTarefa, ETipoStatus? status)
        {
            if (IdTarefa == 0) { throw new Exception("Id da tarefa não informado"); }
            if (!status.HasValue) { throw new Exception("Status da tarefa não informado"); }

            var TarefaAtual = _tarefaGateway.BuscarPorId(IdTarefa);

            if (TarefaAtual == null) { throw new Exception("Tarefa não encontrada com o Id informado"); }

            TarefaAtual.Status = status.Value;

            return _tarefaGateway.Alterar(TarefaAtual);
        }

        public Tarefa AtribuaUmResponsavel(int IdTarefa, ETipoStatus? status, int IdResponsavel)
        {
            if (IdTarefa == 0) { throw new Exception("Id da tarefa não informado"); }
            if (IdResponsavel == 0) { throw new Exception("Id do responsável não informado"); }
            if (!status.HasValue) { throw new Exception("Status da tarefa não informado"); }

            var TarefaAtual = _tarefaGateway.BuscarPorId(IdTarefa);
            var Responsavel = _usuarioGateway.BuscarPorId(IdResponsavel);

            if (TarefaAtual == null) { throw new Exception("Tarefa não encontrada com o Id informado"); }
            if (Responsavel == null) { throw new Exception("Responsável não encontrado com o Id informado"); }

            TarefaAtual.Status = status.Value;
            TarefaAtual.Responsavel = Responsavel;

            return _tarefaGateway.Alterar(TarefaAtual);
        }

        public TarefaDAO RegistreTarefa(TarefaDAO tarefa) 
        {
            if(tarefa == null) { throw new Exception(MensagensValidacoes.Tarefa_DAO_Nula); }
            
            if (!VerifiqueSeUsuarioExiste(tarefa.CriadorId)) { throw new Exception(MensagensValidacoes.Tarefa_Criador); }

            return tarefa;
        }

        private bool VerifiqueSeUsuarioExiste(int idUsuario) 
        {
            return _usuarioGateway.BuscarPorId(idUsuario) != null;
        }
    }
}
