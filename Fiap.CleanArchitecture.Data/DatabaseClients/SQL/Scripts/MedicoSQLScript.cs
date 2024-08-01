using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class MedicoSQLScript
    {
        public static string VerificarUsuario => @"
        
            SELECT TOP 1 NOME, EMAIL, CPF, CRM, SENHA, PAPEL 
            FROM USUARIOS WITH (NOLOCK)
            WHERE EMAIL = @EMAIL AND SENHA = @SENHA ORDER BY 1 ASC

        ";

        public static string BuscarTodos => @"

            SELECT
            	ID AS Id,
               	DATA_CRIACAO AS DataCriacao,
            	NOME AS Nome,
                CPF as 'Cpf',
                CRM as 'Crm',
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
                CPF as 'Cpf',
                CRM as 'Crm',
               	EMAIL AS Email,
               	SENHA AS Senha,
            	PAPEL AS Papel
            FROM USUARIOS WITH (NOLOCK)
            WHERE ID = @ID

        ";

        public static string Criar => @"

            INSERT INTO USUARIOS (DATA_CRIACAO, NOME, CPF, CRM, EMAIL, SENHA, PAPEL) 
            VALUES (GETDATE(), @NOME, @CPF, @CRM, @EMAIL, @SENHA, @PAPEL)

        ";

        public static string Alterar => @"

            UPDATE USUARIOS 
            SET NOME = @NOME, CPF = @CPF, CRM = @CRM, EMAIL = @EMAIL, PAPEL = @PAPEL 
            WHERE ID = @ID

        ";

        public static string Excluir => @"

            DELETE FROM USUARIOS WHERE ID = @ID
        
        ";

        public static string BuscarMedicosDisponibilidade => @"
             select Distinct
	                
                 USUMED.Nome,
                 USUMED.CRM, 
                 USUMED.EMAIL, 
                 AGMED.DIA as 'Dia', 
                 AGMED.MESANO as 'Mes',
                 AGMED.DIADISPONIVEL 'Dia Disponibilidade',
                 AGEMEDIA.HORARIO as 'Hora' , 
                 AGEMEDIA.HORARIODISPONIVEL as 'Horario Disponibilidade'

                 from USUARIOS USUMED
                 join AGENDA_MEDICO_MES AGMED on AGMED.MEDICOID = USUMED.ID
                 join AGENDA_MEDICO_DIA AGEMEDIA on AGEMEDIA.AGENDAMEDICOID = AGMED.ID
        ";
    }
}
