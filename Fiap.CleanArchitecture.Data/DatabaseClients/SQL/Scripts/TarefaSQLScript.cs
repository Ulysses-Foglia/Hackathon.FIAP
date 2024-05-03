namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class TarefaSQLScript
    {
        public static string BuscarTodos => @"

            SELECT
            	T.ID AS Id,
            	T.DATA_CRIACAO AS DataCriacao,
            	T.TITULO AS Titulo,
            	T.DATA_INICIO AS DataInicio,
            	T.DATA_FIM AS DataFim
            FROM TAREFAS T WITH (NOLOCK)

        ";

        public static string BuscarPorId => @"

            SELECT
            	T.ID AS Id,
            	T.DATA_CRIACAO AS DataCriacao,
            	T.TITULO AS Titulo,
            	T.DATA_INICIO AS DataInicio,
            	T.DATA_FIM AS DataFim
            FROM TAREFAS T WITH (NOLOCK)
            WHERE T.ID = @ID

        ";

        public static string Criar => @"

            INSERT INTO TAREFAS (DATA_CRIACAO, TITULO, DATA_INICIO, DATA_FIM, ID_CRIADOR) 
            VALUES (GETDATE(), @TITULO, @DATA_INICIO, @DATA_FIM, @ID_CRIADOR)

        ";

        public static string Alterar => @"

            UPDATE TAREFAS
            SET TITULO = @TITULO,
            DATA_INICIO = @DATA_INICIO,
            DATA_FIM = @DATA_FIM
            WHERE ID = @ID

        ";

        public static string Excluir => @"

            DELETE FROM TAREFAS WHERE ID = @ID
        
        ";
    }
}
