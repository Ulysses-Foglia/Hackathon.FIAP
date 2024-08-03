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
            where PAPEL like '%Medico%'
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
            and USUARIOS.PAPEL like '%Medico%'
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
             select distinct
             USUMED.ID as 'MedicoId',
             USUMED.Nome,
             USUMED.CRM, 
             USUMED.EMAIL, 
             AGMED.ID as 'AgendaMedicoMesId',
             AGMED.DIA,
             AGMED.MESANO, 
             AGMED.DIADISPONIVEL, 
             AGEMEDIA.ID as 'AgendaMedicoDiaId',
             AGEMEDIA.HORARIO, 
             AGEMEDIA.HORARIODISPONIVEL

             from USUARIOS USUMED
             join AGENDA_MEDICO_MES AGMED on AGMED.MEDICOID = USUMED.ID
             join AGENDA_MEDICO_DIA AGEMEDIA on AGEMEDIA.AGENDAMEDICOID = AGMED.ID
             where USUMED.PAPEL like '%Medico%'
        ";
    }
}
