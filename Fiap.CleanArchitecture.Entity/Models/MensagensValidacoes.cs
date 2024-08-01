namespace Fiap.CleanArchitecture.Entity.Models
{
    public static class MensagensValidacoes
    {
        public static string Usuario_Nome = "Nome deve possuir no máximo 100 caracteres!";
        public static string Usuario_Email = "Email no formato incorreto!";
        public static string Usuario_Senha = "Senha deve possuir entre 6 e 20 caracteres!";
        public static string Usuario_Papel = "Papel inválido! Verifique o formato correto.";
        public static string Usuario_Papel_invalido_medico = "Papel inválido para o médico!";
        public static string Usuario_RelacaoTarefas = "Usuario possui relação com tarefas, não é possivel excluir.";       
        public static string Usuario_Crm_Inavalido = "O Crm informado está inválido";
        public static string Usuario_Agenda_Inavalido = "A Agenda informada está inválida";
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
        public static string Tests_UsuarioInvalido = "Usuário inválido.";
        public static string Tests_TextoExemplo = "is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        public static string Agenda_Mes_IdMedico = "É preciso informar um médico";
        public static string Agenda_Mes_MesAno = "É preciso informar um mes e ano para cadastro";
        public static string Agenda_Mes_Dia = "É preciso informar um dia valido para cadastro";
        public static string Agenda_Mes_DiaDisponivel = "É preciso informar a diponibilidade do dia para cadastro";
        public static string Agenda_Mes_DiaAgenda = "É preciso informar ao menos um horario para cadastro";
        public static string Agenda_Dia_IdAgenda = "É preciso informar a identificação da agenda para cadastrar o horario";
        public static string Agenda_Dia_HoraDisponivel = "É preciso informar a diponibilidade da hora e minuto para cadastrar o horario";
        public static string Agenda_Dia_Horario = "É preciso informar um horário válido para cadastro";
        public static string Medico_RelacaoAgenda = "Medico possui agendas registradas não é possivel excluir";
    }
}
