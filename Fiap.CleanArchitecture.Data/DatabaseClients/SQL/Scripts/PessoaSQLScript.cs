namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class PessoaSQLScript
    {
        public static string BuscarTodos => @"

            SELECT
            	P.ID AS Id,
            	P.DATA_CRIACAO AS DataCriacao,
            	P.NOME AS Nome,
               	U.ID AS UsuarioId,
               	U.DATA_CRIACAO AS UsuarioDataCriacao,
               	U.EMAIL AS UsuarioEmail
            FROM PESSOAS P WITH (NOLOCK)
            	LEFT JOIN USUARIOS U WITH (NOLOCK) ON P.ID_USUARIO = U.ID

        ";

        public static string BuscarPorId => @"

            SELECT
            	P.ID AS Id,
            	P.DATA_CRIACAO AS DataCriacao,
            	P.NOME AS Nome,
               	U.ID AS UsuarioId,
               	U.DATA_CRIACAO AS UsuarioDataCriacao,
               	U.EMAIL AS UsuarioEmail
            FROM PESSOAS P WITH (NOLOCK)
            	LEFT JOIN USUARIOS U WITH (NOLOCK) ON P.ID_USUARIO = U.ID
            WHERE P.ID = @ID

        ";

        public static string Criar => @"

            INSERT INTO PESSOAS (DATA_CRIACAO, NOME) VALUES (GETDATE(), @NOME)

        ";

        public static string Alterar => @"

            UPDATE PESSOAS SET NOME = @NOME, ID_USUARIO = @ID_USUARIO WHERE ID = @ID

        ";

        public static string Excluir => @"

            DELETE FROM PESSOAS WHERE ID = @ID
        
        ";
    }
}
