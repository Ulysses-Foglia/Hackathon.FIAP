using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
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
            if (idTarefa == 0)
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_IdNulo);

            if (string.IsNullOrEmpty(situacao))
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_Nula);

            if (!Enum.TryParse(situacao, out TipoStatus enumTipoStatus))
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_Invalida);

            var tarefaAtual = _tarefaGateway.BuscarPorId(idTarefa);

            if (tarefaAtual == null)
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_NaoEncontrada);

            tarefaAtual.Status = enumTipoStatus;

            return _tarefaGateway.Alterar(tarefaAtual);
        }

        public Tarefa AtribuaUmResponsavel(int IdTarefa, ETipoStatus? status, int IdResponsavel)
        {
            if (idTarefa == 0)
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_IdNulo);

            if (string.IsNullOrEmpty(situacao))
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_Nula);

            if (!Enum.TryParse(situacao, out TipoStatus enumTipoStatus))
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_Invalida);

            if (idResponsavel == 0)
                throw new Exception(MensagensValidacoes.Tarefa_Responsavel_IdNulo);

            var tarefaAtual = _tarefaGateway.BuscarPorId(idTarefa);
            var responsavel = _usuarioGateway.BuscarPorId(idResponsavel);

            if (tarefaAtual == null) 
                throw new Exception(MensagensValidacoes.Tarefa_Situacao_NaoEncontrada);
            
            if (responsavel == null)
                throw new Exception(MensagensValidacoes.Tarefa_Responsavel_NaoEncontrado);

            tarefaAtual.Status = enumTipoStatus;
            tarefaAtual.Responsavel = responsavel;

            return _tarefaGateway.Alterar(tarefaAtual);
        }

        public TarefaDAO RegistreTarefa(TarefaDAO tarefa) 
        {
            if(tarefa == null)
                throw new Exception(MensagensValidacoes.Tarefa_DAO_Nula);
            
            if (!VerifiqueSeUsuarioExiste(tarefa.CriadorId))
                throw new Exception(MensagensValidacoes.Tarefa_Criador);

            return tarefa;
        }

        public Tarefa Aprovar(int id)
        {
            var tarefa = _tarefaGateway.BuscarPorId(id);

            if (tarefa.Status != TipoStatus.PendenteAprovacao)
                throw new Exception(MensagensValidacoes.Tarefa_Status_Aprovacao);

            _tarefaGateway.Aprovar(id);

            return _tarefaGateway.BuscarPorId(tarefa.Id);
        }

        private bool VerifiqueSeUsuarioExiste(int idUsuario) 
        {
            return _usuarioGateway.BuscarPorId(idUsuario) != null;
        }

        public void ExcluirTarefa(int id)
        {
            _tarefaGateway.Excluir(id);
        }
    }
}
