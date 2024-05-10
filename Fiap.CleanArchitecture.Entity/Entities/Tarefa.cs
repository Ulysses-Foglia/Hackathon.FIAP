using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.DTO;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Tarefa : EntityBase
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Prazo Prazo { get; set; }
        public TipoStatus Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Usuario Criador { get; set; }
        public Usuario Responsavel { get; set; }

        public Tarefa() { }

        public Tarefa(TarefaDAO tarefaDAO)
        {
            if (!TituloValido(tarefaDAO.Titulo))
                throw new Exception(MensagensValidacoes.Tarefa_Titulo);
            
            if (!DescricaoValido(tarefaDAO.Descricao))
                throw new Exception(MensagensValidacoes.Tarefa_Descricao);

            if (!PrazoValido(tarefaDAO.PrazoValor, tarefaDAO.PrazoUnidade, out Prazo prazo))
                throw new Exception(MensagensValidacoes.Tarefa_Prazo);

            if (!StatusValido(tarefaDAO.Status, out TipoStatus status))
                throw new Exception(MensagensValidacoes.Tarefa_Prazo);
            
            Descricao = tarefaDAO.Descricao;
            Titulo = tarefaDAO.Titulo;
            Prazo = new Prazo() { Valor = prazo.Valor, Unidade = prazo.Unidade };
            Status = status;
            Criador = new Usuario() { Id = tarefaDAO.CriadorId };
            DataCriacao = DateTime.Now;
        }

        public Tarefa(TarefaAlterarDAO tarefaAlterarDAO)
        {
            if (!TituloValido(tarefaAlterarDAO.Titulo))
                throw new Exception(MensagensValidacoes.Tarefa_Titulo);

            if (!DescricaoValido(tarefaAlterarDAO.Descricao))
                throw new Exception(MensagensValidacoes.Tarefa_Descricao);

            if (!PrazoValido(tarefaAlterarDAO.PrazoValor, tarefaAlterarDAO.PrazoUnidade, out Prazo prazo))
                throw new Exception(MensagensValidacoes.Tarefa_Prazo);

            if (!StatusValido(tarefaAlterarDAO.Status, out TipoStatus status))
                throw new Exception(MensagensValidacoes.Tarefa_Prazo);

            if (!DataInicioValida(tarefaAlterarDAO.DataInicio, out DateTime dataInicio))
                throw new Exception(MensagensValidacoes.Tarefa_DataInicio);

            if (!DataFimValida(tarefaAlterarDAO.DataFim, out DateTime dataFim))
                throw new Exception(MensagensValidacoes.Tarefa_DataFim);

            Id = tarefaAlterarDAO.Id;
            Titulo = tarefaAlterarDAO.Titulo;
            Descricao = tarefaAlterarDAO.Descricao;
            Prazo = new Prazo() { Valor = prazo.Valor, Unidade = prazo.Unidade };
            Status = status;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Responsavel = new Usuario() { Id = tarefaAlterarDAO.ResponsavelId };
        }

        private bool TituloValido(string titulo) => titulo.Length <= 100;

        private bool DescricaoValido(string desc) => desc.Length <= 2000;

        private bool PrazoValido(int valor, string unidadeString, out Prazo prazo)
        {
            bool valido;
            var unidade = TipoUnidade.m;

            valido = valor > 0 &&
            Enum.TryParse(unidadeString, out unidade);
            
            prazo = new Prazo();

            if (valido)
                prazo = new Prazo() { Valor = valor, Unidade = unidade };

            return valido;
        }

        private bool StatusValido(string statusString, out TipoStatus status) 
            => Enum.TryParse(statusString, out status);

        private bool DataInicioValida(string dataInicioString, out DateTime dataInicio)
            => DateTime.TryParse(dataInicioString, out dataInicio);

        private bool DataFimValida(string dataFimString, out DateTime dataFim)
            => DateTime.TryParse(dataFimString, out dataFim);
    }
}
