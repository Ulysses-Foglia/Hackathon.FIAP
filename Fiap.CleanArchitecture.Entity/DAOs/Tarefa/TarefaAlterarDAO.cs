namespace Fiap.CleanArchitecture.Entity.DAOs.Tarefa
{
    public class TarefaAlterarDAO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int PrazoValor { get; set; }
        public string PrazoUnidade { get; set; }
        public string Status { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int ResponsavelId { get; set; }
    }
}
