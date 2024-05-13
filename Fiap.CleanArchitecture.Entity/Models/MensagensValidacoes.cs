namespace Fiap.CleanArchitecture.Entity.Models
{
    public static class MensagensValidacoes
    {
        public static string Usuario_Nome = "Nome deve possuir no máximo 100 caracteres!";
        public static string Usuario_Email = "Email no formato incorreto!";
        public static string Usuario_Senha = "Senha deve possuir entre 6 e 20 caracteres!";
        public static string Usuario_Papel = "Papel inválido! Verifique o formato correto.";
        
        public static string Tarefa_Titulo = "Titulo deve possuir no máximo 100 caracteres!";
        public static string Tarefa_Descricao = "Descricao deve possuir no máximo 2000 caracteres!";
        public static string Tarefa_Prazo = "Prazo inválido! Verifique o formato correto.";
        public static string Tarefa_Status = "Status inválido! Verifique o formato correto.";
        public static string Tarefa_Status_Aprovacao = "Tarefa não está no status correto para aprovação!";
        public static string Tarefa_Situacao_IdNulo = "Id da tarefa não informado!";
        public static string Tarefa_Situacao_Nula = "Status da tarefa não informado!";
        public static string Tarefa_Situacao_Invalida = "Situação informada não é válida!";
        public static string Tarefa_Situacao_NaoEncontrada = "Tarefa não encontrada com o Id informado!";
        public static string Tarefa_DataInicio = "Data Inicio está em formato incorreto!";
        public static string Tarefa_DataFim = "Data Fim está em formato incorreto!";
        public static string Tarefa_Criador = "Criador(a) da tarefa inválido ou inexistente!";
        public static string Tarefa_Responsavel = "Responsavel da tarefa inválido ou inexistente!";
        public static string Tarefa_Responsavel_IdNulo = "Id do responsável não informado!";
        public static string Tarefa_Responsavel_NaoEncontrado = "Responsável não encontrado com o Id informado!";
        public static string Tarefa_DAO_Nula = "Objeto para criação da tarefa inexistente!";
    }
}
