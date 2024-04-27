namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Tarefa : EntityBase
    {
        public required string Titulo { get; set; }
        public required DateTime DataInicio { get; set; }
        public required DateTime DataFim { get; set; }
        public required Pessoa Criador { get; set; }
        public required IEnumerable<Pessoa> Executores { get; set; }

        public Tarefa(string titulo, Pessoa criador)
        {
            Titulo = titulo;
            Criador = criador;
            Executores = [];
        }
    }
}
