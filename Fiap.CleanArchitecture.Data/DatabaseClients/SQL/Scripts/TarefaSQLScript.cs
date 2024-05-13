namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class TarefaSQLScript
    {
        public static string BuscarTodos => @"

            SELECT
            	T.ID AS Id,
            	T.DATA_CRIACAO AS DataCriacao,
            	T.TITULO AS Titulo,
                T.DESCRICAO AS Descricao,
            	T.PRAZO_VALOR AS PrazoValor,
            	T.PRAZO_UNIDADE AS PrazoUnidade,
            	T.[STATUS] AS [Status],
            	T.DATA_INICIO AS DataInicio,
            	T.DATA_FIM AS DataFim,
            	U_C.NOME AS CriadorNome,
            	U_C.EMAIL AS CriadorEmail,
            	U_C.PAPEL AS CriadorPapel,
            	U_R.NOME AS ResponsavelNome,
            	U_R.EMAIL AS ResponsavelEmail,
            	U_R.PAPEL AS ResponsavelPapel
            FROM TAREFAS T WITH (NOLOCK)
                LEFT JOIN USUARIOS U_C WITH (NOLOCK) ON T.ID_CRIADOR = U_C.ID
                LEFT JOIN USUARIOS U_R WITH (NOLOCK) ON T.ID_RESPONSAVEL = U_R.ID

        ";

        public static string BuscarPorId => @"

            SELECT
            	T.ID AS Id,
            	T.DATA_CRIACAO AS DataCriacao,
            	T.TITULO AS Titulo,
                T.DESCRICAO AS Descricao,
            	T.PRAZO_VALOR AS PrazoValor,
            	T.PRAZO_UNIDADE AS PrazoUnidade,
            	T.[STATUS] AS [Status],
            	T.DATA_INICIO AS DataInicio,
            	T.DATA_FIM AS DataFim,
                U_C.ID AS CriadorId,
            	U_C.NOME AS CriadorNome,
            	U_C.EMAIL AS CriadorEmail,
            	U_C.PAPEL AS CriadorPapel,
                U_R.ID AS ResponsavelId,
            	U_R.NOME AS ResponsavelNome,
            	U_R.EMAIL AS ResponsavelEmail,
            	U_R.PAPEL AS ResponsavelPapel
            FROM TAREFAS T WITH (NOLOCK)
                LEFT JOIN USUARIOS U_C WITH (NOLOCK) ON T.ID_CRIADOR = U_C.ID
                LEFT JOIN USUARIOS U_R WITH (NOLOCK) ON T.ID_RESPONSAVEL = U_R.ID
            WHERE T.ID = @ID

        ";

        public static string Criar => @"

            INSERT INTO TAREFAS 
                (DATA_CRIACAO, TITULO, DESCRICAO, PRAZO_VALOR, PRAZO_UNIDADE, 
                [STATUS], DATA_INICIO, DATA_FIM, ID_CRIADOR, ID_RESPONSAVEL) 
            VALUES 
                (GETDATE(), @TITULO, @DESCRICAO, @PRAZO_VALOR, @PRAZO_UNIDADE, 
                @STATUS, @DATA_INICIO, @DATA_FIM, @ID_CRIADOR, @ID_RESPONSAVEL)

        ";

        public static string Alterar => @"

            UPDATE TAREFAS SET
            	TITULO = @TITULO,
                DESCRICAO =@DESCRICAO,
            	PRAZO_VALOR = @PRAZO_VALOR,
            	PRAZO_UNIDADE = @PRAZO_UNIDADE,
            	[STATUS] = @STATUS,
            	DATA_INICIO = @DATA_INICIO,
            	DATA_FIM = @DATA_FIM,
            	ID_RESPONSAVEL = @ID_RESPONSAVEL
            WHERE ID = @ID

        ";

        public static string Excluir => @"

            DELETE FROM TAREFAS WHERE ID = @ID
        
        ";

        public static string Aprovar => @"

            UPDATE TAREFAS SET [STATUS] = @STATUS WHERE ID = @ID
        
        ";
    }
}
