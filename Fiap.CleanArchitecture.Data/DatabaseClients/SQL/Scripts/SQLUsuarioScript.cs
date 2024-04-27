namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class SQLUsuarioScript
    {
        public static string BuscarTodos => @"

            SELECT
            	ID AS Id,
            	DATA_CRIACAO AS DataCriacao,
            	EMAIL AS Email,
            	SENHA AS Senha
            FROM USUARIOS WITH (NOLOCK)

        ";
    }
}
