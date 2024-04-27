namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Pessoa : EntityBase
    {
        public string Nome { get; set; }
        public Usuario Usuario { get; set; }
        public IEnumerable<Tarefa> Tarefas { get; set; }

        public Pessoa(string nome)
        {
            Nome = nome;
            Usuario = null;
            Tarefas = [];
        }
    }
}
