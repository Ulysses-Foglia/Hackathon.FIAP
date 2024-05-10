using Fiap.CleanArchitecture.Entities;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.DTO;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Fiap.CleanArchitecture.Util;
using System.Net.NetworkInformation;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Tarefa : EntityBase
    {
        public string Titulo { get; set; }
        public Prazo Prazo { get; set; }
        public ETipoStatus Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Usuario Criador { get; set; }
        public Usuario Responsavel { get; set; }

        public Tarefa() { }

        public Tarefa(TarefaDAO tarefaDAO)
        {
            ValidarEntity(tarefaDAO);

            if (!TituloValido(tarefaDAO.Titulo))
                throw new Exception(MensagensValidacoes.Tarefa_Titulo);

            if (!PrazoValido(tarefaDAO.PrazoValor, tarefaDAO.PrazoUnidade, out Prazo prazo))
                throw new Exception(MensagensValidacoes.Tarefa_Prazo);

            if (!StatusValido(tarefaDAO.Status, out ETipoStatus status))
                throw new Exception(MensagensValidacoes.Tarefa_Status);

            Titulo = tarefaDAO.Titulo;
            Prazo = new Prazo() { Valor = prazo.Valor, Unidade = prazo.Unidade };
            Status = status;
            Criador = new Usuario() { Id = tarefaDAO.CriadorId };
        }

        public Tarefa(TarefaAlterarDAO tarefaAlterarDAO)
        {
            ValidarEntity(tarefaAlterarDAO);

            if (!TituloValido(tarefaAlterarDAO.Titulo))
                throw new Exception(MensagensValidacoes.Tarefa_Titulo);

            if (!PrazoValido(tarefaAlterarDAO.PrazoValor, tarefaAlterarDAO.PrazoUnidade, out Prazo prazo))
                throw new Exception(MensagensValidacoes.Tarefa_Prazo);

            if (!StatusValido(tarefaAlterarDAO.Status, out ETipoStatus status))
                throw new Exception(MensagensValidacoes.Tarefa_Status);

            if (!DataInicioValida(tarefaAlterarDAO.DataInicio, out DateTime dataInicio))
                throw new Exception(MensagensValidacoes.Tarefa_DataInicio);

            if (!DataFimValida(tarefaAlterarDAO.DataFim, out DateTime dataFim))
                throw new Exception(MensagensValidacoes.Tarefa_DataFim);

            Id = tarefaAlterarDAO.Id;
            Titulo = tarefaAlterarDAO.Titulo;
            Prazo = new Prazo() { Valor = prazo.Valor, Unidade = prazo.Unidade };
            Status = status;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Responsavel = new Usuario() { Id = tarefaAlterarDAO.ResponsavelId };
        }

        private bool TituloValido(string titulo) => titulo.Length <= 100;

        private bool PrazoValido(int valor, string unidadeString, out Prazo prazo)
        {
            bool valido;
            var unidade = ETipoUnidade.m;

            valido = valor > 0 &&
            Enum.TryParse(unidadeString, out unidade);
            prazo = new Prazo();

            if (valido)
                prazo = new Prazo() { Valor = valor, Unidade = unidade };

            return valido;
        }

        private bool PrazoValido(Prazo prazo)
        {
            bool valido;
            var unidade = ETipoUnidade.m;

            valido = prazo.Valor > 0 && Enum.TryParse(Ferramentas.GetGenericEnumDescription(prazo.Unidade), out unidade);
            prazo = new Prazo();

            if (valido)
                prazo = new Prazo() { Valor = prazo.Valor, Unidade = unidade };

            return valido;
        }

        private bool StatusValido(string statusString, out ETipoStatus status)
            => Enum.TryParse(statusString, out status);

        private bool DataInicioValida(string dataInicioString, out DateTime dataInicio)
            => DateTime.TryParse(dataInicioString, out dataInicio);

        private bool DataFimValida(string dataFimString, out DateTime dataFim)
            => DateTime.TryParse(dataFimString, out dataFim);

        public void ValidarEntity(TarefaDAO tarefaDAO)
        {
            AssertionConcern.AssertArgumentTrue(TituloValido(tarefaDAO.Titulo), MensagensValidacoes.Tarefa_Titulo);
            AssertionConcern.AssertArgumentTrue(PrazoValido(tarefaDAO.PrazoValor, tarefaDAO.PrazoUnidade, out Prazo prazo), MensagensValidacoes.Tarefa_Prazo);
            AssertionConcern.AssertArgumentTrue(StatusValido(tarefaDAO.Status, out ETipoStatus status), MensagensValidacoes.Tarefa_Status);
        }

        public void ValidarEntity(TarefaAlterarDAO tarefaAlterarDAO)
        {
            AssertionConcern.AssertArgumentTrue(TituloValido(tarefaAlterarDAO.Titulo), MensagensValidacoes.Tarefa_Titulo);
            AssertionConcern.AssertArgumentTrue(PrazoValido(tarefaAlterarDAO.PrazoValor, tarefaAlterarDAO.PrazoUnidade, out Prazo prazo), MensagensValidacoes.Tarefa_Prazo);
            AssertionConcern.AssertArgumentTrue(StatusValido(tarefaAlterarDAO.Status, out ETipoStatus status), MensagensValidacoes.Tarefa_Status);
            AssertionConcern.AssertArgumentTrue(DataInicioValida(tarefaAlterarDAO.DataInicio, out DateTime dataInicio), MensagensValidacoes.Tarefa_DataInicio);
            AssertionConcern.AssertArgumentTrue(DataFimValida(tarefaAlterarDAO.DataFim, out DateTime dataFim), MensagensValidacoes.Tarefa_DataFim);

        }
    }
}
