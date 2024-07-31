// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoScript.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts
{
    public class AgendaMedicoSQLScript
    {
        /// <summary>
        /// VALUES (@MEDICOID, @MESANO, @DIA, @DIADISPONIVEL, GETDATE())
        /// </summary>
        public static string CriarAgenda => @"
                
                INSERT INTO AGENDA_MEDICO_MES (MEDICOID, MESANO, DIA, DIADISPONIVEL, DATA_CRIACAO)
                OUTPUT INSERTED.ID 
                VALUES (@MEDICOID, @MESANO, @DIA, @DIADISPONIVEL, GETDATE())
        ";

        /// <summary>
        /// VALUES (@HORARIO, @HORARIODISPONIVEL, GETDATE(), @PACIENTEID, @AGENDAMEDICOID)
        /// </summary>
        public static string CriarHorarioAgenda => @"
                
                INSERT INTO AGENDA_MEDICO_DIA (HORARIO, HORARIODISPONIVEL, DATA_CRIACAO, PACIENTEID, AGENDAMEDICOID)
                OUTPUT INSERTED.ID 
                VALUES (@HORARIO, @HORARIODISPONIVEL, GETDATE(), @PACIENTEID, @AGENDAMEDICOID)
    
         ";

        /// <summary>
        /// Busca completa das agendas do médico
        /// </summary>
        public static string BuscarTodasAgendasDosMedicos => @"
            
                SELECT 
                AM.ID
                AM.MEDICOID, 
                AM.MESANO, 
                AM.DIA, 
                AM.DIADISPONIVEL, 
                AM.DATA_CRIACAO
                FROM AGENDA_MEDICO_MES AM WITH (NOLOCK)
        ";

        /// <summary>
        /// @MEDICOID (ID do Médico)
        /// </summary>
        public static string BuscaAgendasDoMedicoPorID => @"
            
                SELECT 
                AM.ID
                AM.MEDICOID, 
                AM.MESANO, 
                AM.DIA, 
                AM.DIADISPONIVEL, 
                AM.DATA_CRIACAO
                FROM AGENDA_MEDICO_MES AM WITH (NOLOCK)
                WHERE AM.MEDICOID = @MEDICOID 
        ";

        /// <summary>
        ///  @AGENDAMEDICOID (ID da Agenda do médico)
        /// </summary>
        public static string ObtenhaHorariosDaAgendaPeloIdAgenda => @"
            
               SELECT
               AD.ID
               AD.HORARIO, 
               AD.HORARIODISPONIVEL, 
               AD.DATA_CRIACAO, 
               AD.PACIENTEID, 
               AD.AGENDAMEDICOID
               FROM AGENDA_MEDICO_DIA AD WITH (NOLOCK)
               WHERE AD.AGENDAMEDICOID = @AGENDAMEDICOID
        ";

        /// <summary>
        /// @DIADISPONIVEL (Enum DiaDisponivel)
        /// @ID (ID da Agenda)      
        /// </summary>
        public static string AtualizeAgendaMedicoPorId => @"
                
              UPDATE AGENDA_MEDICO_MES SET DIADISPONIVEL = @DIADISPONIVEL WHERE ID = @ID

        ";



    }
}
