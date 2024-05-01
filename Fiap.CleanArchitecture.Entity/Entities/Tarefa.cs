using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Tarefa : EntityBase
    {
        public string Titulo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Pessoa Criador { get; set; }
        public IEnumerable<Pessoa> Executores { get; set; }

        private readonly string MensagemCriadorInvalido
            = "Criador(a) da tarefa inválido ou inexistente!";

        private readonly string MensagemTituloInvalido
            = "O titulo deve possuir no máximo 100 caracteres!";

        private readonly string MensagemDataInicioInvalida
            = "A data inicio está em formato incorreto!";

        private readonly string MensagemDataFimInvalida
            = "A data fim está em formato incorreto!";

        public Tarefa() { }

        public Tarefa(string titulo, Pessoa criador)
        {
            if (!TituloValido(titulo))
                throw new Exception(MensagemTituloInvalido);

            Titulo = titulo;
            Criador = criador;
            Executores = [];
        }

        public Tarefa(TarefaDAO tarefaDAO, Pessoa criador)
        {
            if (!CriadorValido(criador))
                throw new Exception(MensagemCriadorInvalido);

            if (!TituloValido(tarefaDAO.Titulo))
                throw new Exception(MensagemTituloInvalido);

            if (!DataInicioValida(tarefaDAO.DataInicio, out DateTime dataInicio))
                throw new Exception(MensagemDataInicioInvalida);

            if (!DataFimValida(tarefaDAO.DataFim, out DateTime dataFim))
                throw new Exception(MensagemDataFimInvalida);

            Titulo = tarefaDAO.Titulo;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Criador = criador;
        }

        public Tarefa(TarefaAlterarDAO tarefaAlterarDAO)
        {
            if (!TituloValido(tarefaAlterarDAO.Titulo))
                throw new Exception(MensagemTituloInvalido);

            if (!DataInicioValida(tarefaAlterarDAO.DataInicio, out DateTime dataInicio))
                throw new Exception(MensagemDataInicioInvalida);

            if (!DataFimValida(tarefaAlterarDAO.DataFim, out DateTime dataFim))
                throw new Exception(MensagemDataFimInvalida);

            Id = tarefaAlterarDAO.Id;
            Titulo = tarefaAlterarDAO.Titulo;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        private bool CriadorValido(Pessoa criador) => criador != null;

        private bool TituloValido(string titulo) => titulo.Length <= 100;

        private bool DataInicioValida(string dataInicioString, out DateTime dataInicio)
            => DateTime.TryParse(dataInicioString, out dataInicio);

        private bool DataFimValida(string dataFimString, out DateTime dataFim)
            => DateTime.TryParse(dataFimString, out dataFim);
    }
}
