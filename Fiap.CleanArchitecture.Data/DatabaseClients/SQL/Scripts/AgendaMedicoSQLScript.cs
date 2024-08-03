// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoScript.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

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
                AM.ID,
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
                AM.ID,
                AM.MEDICOID, 
                AM.MESANO, 
                AM.DIA, 
                AM.DIADISPONIVEL, 
                AM.DATA_CRIACAO
                FROM AGENDA_MEDICO_MES AM WITH (NOLOCK)
                WHERE AM.MEDICOID = @MEDICOID 
        ";

        /// <summary>
        /// @LIMITE (lIMITE DE LINHAS)
        /// </summary>
        public static string BuscaAgendasDosMedicos => @"
            
                SELECT TOP (@LIMITE)
                AM.ID,
                AM.MEDICOID, 
                AM.MESANO, 
                AM.DIA, 
                AM.DIADISPONIVEL, 
                AM.DATA_CRIACAO
                FROM AGENDA_MEDICO_MES AM WITH (NOLOCK)
        ";

        /// <summary>
        /// @MEDICOID (ID do Médico)
        /// @ID (ID AGENDA)
        /// </summary>
        public static string BuscaAgendaDoMedicoPorIdEhMedico => @"
            
                SELECT 
                AM.ID,
                AM.MEDICOID, 
                AM.MESANO, 
                AM.DIA, 
                AM.DIADISPONIVEL, 
                AM.DATA_CRIACAO
                FROM AGENDA_MEDICO_MES AM WITH (NOLOCK)
                WHERE AM.MEDICOID = @MEDICOID and ID = @ID 
        ";


        /// <summary>
        /// @MEDICOID (ID do Médico)
        /// @DIA (Dia da Agenda)
        /// @MESANO (Mes eh Ano)
        /// </summary>
        public static string BuscaAgendasDoMedicoPorIdEhDiaEhMes => @"
            
                SELECT 
                AM.ID,
                AM.MEDICOID, 
                AM.MESANO, 
                AM.DIA, 
                AM.DIADISPONIVEL, 
                AM.DATA_CRIACAO
                FROM AGENDA_MEDICO_MES AM WITH (NOLOCK)
                WHERE AM.MEDICOID = @MEDICOID and AM.DIA = @DIA AND AM.MESANO = @MESANO
        ";


        /// <summary>
        ///  @AGENDAMEDICOID (ID da Agenda do médico)
        /// </summary>
        public static string ObtenhaHorariosDaAgendaPeloIdAgenda => @"
            
               SELECT
               AD.ID,
               AD.HORARIO, 
               AD.HORARIODISPONIVEL, 
               AD.DATA_CRIACAO, 
               AD.PACIENTEID, 
               AD.AGENDAMEDICOID
               FROM AGENDA_MEDICO_DIA AD WITH (NOLOCK)
               WHERE AD.AGENDAMEDICOID = @AGENDAMEDICOID
        ";


        /// <summary>
        ///  @ID (ID da Horario do médico)
        /// </summary>
        public static string ObtenhaHorarioDaAgendaPeloId => @"
            
               SELECT
               AD.ID,
               AD.HORARIO, 
               AD.HORARIODISPONIVEL, 
               AD.DATA_CRIACAO, 
               AD.PACIENTEID, 
               AD.AGENDAMEDICOID
               FROM AGENDA_MEDICO_DIA AD WITH (NOLOCK)
               WHERE AD.ID = @ID
        ";

        /// <summary>
        ///  @AGENDAMEDICOID (ID da Agenda do médico)
        /// </summary>
        public static string ObtenhaVersaoLinhaHorariosDaAgendaPeloId => @"
            
               SELECT
               AD.VERSAOLINHA
               FROM AGENDA_MEDICO_DIA AD WITH (NOLOCK)
               WHERE AD.ID = @ID
        ";


        /// <summary>
        /// @DIADISPONIVEL (Enum DiaDisponivel)
        /// @ID (ID da Agenda)      
        /// </summary>
        public static string AtualizeAgendaMedicoPorId => @"
                
              UPDATE AGENDA_MEDICO_MES SET DIADISPONIVEL = @DIADISPONIVEL WHERE ID = @ID

        ";

        /// <summary>
        /// @PACIENTEID (ID do paciente)
        /// @HORARIODISPONIVEL (Enum HorarioDisponivel)
        /// @AGENDAMEDICOID (Id agenda do medico)
        /// @ID (Id do horario)
        /// </summary>
        public static string AtualizeHorarioComPasciente => @"
            
             UPDATE  AGENDA_MEDICO_DIA SET PACIENTEID = @PACIENTEID, HORARIODISPONIVEL = @HORARIODISPONIVEL WHERE AGENDAMEDICOID = @AGENDAMEDICOID AND ID = @ID

        ";

        /// <summary>
        /// @AGENDAMEDICOID (Id agenda do medico)
        /// @ID (Id do horario)
        /// </summary>
        public static string AtualizeLibereHorario => @"
            
             UPDATE  AGENDA_MEDICO_DIA SET PACIENTEID = NULL, HORARIODISPONIVEL = 'DISPONIVEL' WHERE AGENDAMEDICOID = @AGENDAMEDICOID AND ID = @ID

        ";

        /// <summary>
        /// @AGENDAMEDICOID (Id agenda do medico)
        /// @ID (Id do horario)
        /// @HORARIO (Horario para atualização formato '00:00')
        /// </summary>
        public static string AtualizeHorario => @"
            
             UPDATE  AGENDA_MEDICO_DIA SET HORARIO = @HORARIO, HORARIODISPONIVEL = 'DISPONIVEL' WHERE AGENDAMEDICOID = @AGENDAMEDICOID AND ID = @ID

        ";

        /// <summary>
        /// Deleta um horário
        /// @ID (Id do Horário)
        /// @AGENDAMEDICOID (Id da Agenda do Médico)
        /// </summary>
        public static string DeleteHorario => @"
            
             DELETE FROM AGENDA_MEDICO_DIA WHERE ID = @ID AND AGENDAMEDICOID = @AGENDAMEDICOID

        ";

        /// <summary>
        /// Deleta uma Angenda
        /// @ID (Id da agenda)
        /// </summary>
        public static string DeleteAgenda => @"
            
             DELETE FROM AGENDA_MEDICO_MES WHERE ID = @ID

        ";
    }
}
