namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class UsuarioSQLScript
    {
        public static string GerarToken => @"

            SELECT 1 FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA

        ";

        public static string BuscarTodos => @"

            SELECT
            	ID AS Id,
            	DATA_CRIACAO AS DataCriacao,
            	EMAIL AS Email,
            	SENHA AS Senha
            FROM USUARIOS WITH (NOLOCK)

        ";

        public static string BuscarPorId => @"

            SELECT
            	ID AS Id,
            	DATA_CRIACAO AS DataCriacao,
            	EMAIL AS Email,
            	SENHA AS Senha
            FROM USUARIOS WITH (NOLOCK)
            WHERE ID = @ID

        ";

        public static string Criar => @"

            INSERT INTO USUARIOS VALUES (GETDATE(), @EMAIL, @SENHA)

        ";

        public static string Alterar => @"

            UPDATE USUARIOS SET EMAIL = @EMAIL WHERE ID = @ID

        ";

        public static string Excluir => @"

            DELETE FROM USUARIOS WHERE ID = @ID
        
        ";
    }
}
