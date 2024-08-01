namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class UsuarioSQLScript
    {
        public static string VerificarUsuario => @"
        
            SELECT TOP 1 NOME, EMAIL, CPF, SENHA, PAPEL 
            FROM USUARIOS WITH (NOLOCK)
            WHERE EMAIL = @EMAIL AND SENHA = @SENHA ORDER BY 1 ASC

        ";

        public static string BuscarTodos => @"

            SELECT
            	ID AS Id,
               	DATA_CRIACAO AS DataCriacao,
            	NOME AS Nome,
                CPF as 'Cpf',              
               	EMAIL AS Email,
               	SENHA AS Senha,
            	PAPEL AS Papel
            FROM USUARIOS WITH (NOLOCK)
            WHERE
             PAPEL like 'Paciente'

        ";

        public static string BuscarPorId => @"

            SELECT
            	ID AS Id,
               	DATA_CRIACAO AS DataCriacao,
            	NOME AS Nome,
                CPF as 'Cpf',               
               	EMAIL AS Email,
               	SENHA AS Senha,
            	PAPEL AS Papel
            FROM USUARIOS WITH (NOLOCK)
            WHERE ID = @ID
            and PAPEL like 'Paciente' 
        ";

        public static string Criar => @"

            INSERT INTO USUARIOS (DATA_CRIACAO, NOME, CPF, EMAIL, SENHA, PAPEL) 
            VALUES (GETDATE(), @NOME, @CPF, @EMAIL, @SENHA, @PAPEL)

        ";

        public static string Alterar => @"

            UPDATE USUARIOS 
            SET NOME = @NOME, CPF = @CPF, EMAIL = @EMAIL, PAPEL = @PAPEL 
            WHERE ID = @ID
            
        ";

        public static string Excluir => @"

            DELETE FROM USUARIOS WHERE ID = @ID
        
        ";
    }
}
