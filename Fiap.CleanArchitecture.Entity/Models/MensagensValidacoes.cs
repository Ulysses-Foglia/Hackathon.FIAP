namespace Fiap.CleanArchitecture.Entity.Models
{
    public static class MensagensValidacoes
    {
        public static string Usuario_Nome = "Nome deve possuir no máximo 100 caracteres!";
        public static string Usuario_Email = "Email no formato incorreto!";
        public static string Usuario_Senha = "Senha deve possuir entre 6 e 20 caracteres!";
        public static string Usuario_Papel = "Papel inválido! Verifique o formato correto.";
        
        public static string Tarefa_Titulo = "Titulo deve possuir no máximo 100 caracteres!";
        public static string Tarefa_Prazo = "Prazo inválido! Verifique o formato correto.";
        public static string Tarefa_Status = "Status inválido! Verifique o formato correto.";
        public static string Tarefa_DataInicio = "Data Inicio está em formato incorreto!";
        public static string Tarefa_DataFim = "Data Fim está em formato incorreto!";
        public static string Tarefa_Criador = "Criador(a) da tarefa inválido ou inexistente!";
        public static string Tarefa_Responsavel = "Responsavel da tarefa inválido ou inexistente!";
    }
}
