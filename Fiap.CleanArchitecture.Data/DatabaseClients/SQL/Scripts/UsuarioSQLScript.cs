namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class UsuarioSQLScript
    {
        public static string VerificarUsuario => @"

            SELECT PAPEL AS Papel FROM USUARIOS WITH (NOLOCK)
            WHERE EMAIL = @EMAIL AND SENHA = @SENHA

        ";

        public static string BuscarTodos => @"

            SELECT
            	ID AS Id,
               	DATA_CRIACAO AS DataCriacao,
            	NOME AS Nome,
               	EMAIL AS Email,
               	SENHA AS Senha,
            	PAPEL AS Papel
            FROM USUARIOS WITH (NOLOCK)

        ";

        public static string BuscarPorId => @"

            SELECT
            	ID AS Id,
               	DATA_CRIACAO AS DataCriacao,
            	NOME AS Nome,
               	EMAIL AS Email,
               	SENHA AS Senha,
            	PAPEL AS Papel
            FROM USUARIOS WITH (NOLOCK)
            WHERE ID = @ID

        ";

        public static string Criar => @"

            INSERT INTO USUARIOS (DATA_CRIACAO, NOME, EMAIL, SENHA, PAPEL) 
            VALUES (GETDATE(), @NOME, @EMAIL, @SENHA, @PAPEL)

        ";

        public static string Alterar => @"

            UPDATE USUARIOS 
            SET NOME = @NOME, EMAIL = @EMAIL, PAPEL = @PAPEL 
            WHERE ID = @ID

        ";

        public static string Excluir => @"

            DELETE FROM USUARIOS WHERE ID = @ID
        
        ";
    }
}
