namespace Fiap.CleanArchitecture.Entity.DAOs.Tarefa
{
    public class TarefaDAO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int PrazoValor { get; set; }
        public string PrazoUnidade { get; set; }
        public string Status { get; set; }
        public int CriadorId { get; set; }
    }
}
