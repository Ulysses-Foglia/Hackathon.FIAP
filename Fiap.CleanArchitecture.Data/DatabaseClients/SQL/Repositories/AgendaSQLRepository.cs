// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaSQLRepository.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Dapper;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Scripts;
using Fiap.CleanArchitecture.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    public class AgendaSQLRepository : Repository
    {
        private readonly IConfiguration _configuration;

        public AgendaSQLRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Cria uma agenda composta com os horarios disponíves
        /// </summary>
        /// <param name="agenda"></param>
        /// <returns></returns>
        public int CrieAgendaDoMedico(AgendaMedicoMes agenda)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.CriarAgenda;

                comd.Parameters.AddWithValue("@MEDICOID", agenda.MedicoId);
                comd.Parameters.AddWithValue("@MESANO", agenda.MesAno);
                comd.Parameters.AddWithValue("@DIA", agenda.Dia);
                comd.Parameters.AddWithValue("@DIADISPONIVEL", agenda.DiaDisponivel.ToString());

                var idGerado = (int)comd.ExecuteScalar();

                if (agenda.DiasDaAgenda.Any() && agenda.DiasDaAgenda.Count > 0) 
                {
                    foreach (var horario in agenda.DiasDaAgenda)
                    {
                        comd.CommandText = AgendaMedicoSQLScript.CriarHorarioAgenda;

                        comd.Parameters.Clear();
                        comd.Parameters.AddWithValue("@HORARIO", horario.Horario);
                        comd.Parameters.AddWithValue("@HORARIODISPONIVEL", horario.HorarioDisponivel.ToString());
                        comd.Parameters.AddWithValue("@PACIENTEID", horario.PacienteId);                       
                        comd.Parameters.AddWithValue("@AGENDAMEDICOID", idGerado);                       
                        comd.ExecuteScalar();
                    }
                }

                trans.Commit();

                return idGerado;
            }
        }

        /// <summary>
        /// Cria um Horario na agenda do Médico
        /// </summary>
        /// <param name="horario"></param>
        /// <returns></returns>
        public int CrieHorarioNaAgendaDoMedico(AgendaMedicoDia horario) 
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;
                comd.CommandText = AgendaMedicoSQLScript.CriarHorarioAgenda;

                comd.Parameters.Clear();
                comd.Parameters.AddWithValue("@HORARIO", horario.Horario);
                comd.Parameters.AddWithValue("@HORARIODISPONIVEL", horario.HorarioDisponivel.ToString());
                comd.Parameters.AddWithValue("@PACIENTEID", horario.PacienteId);
                comd.Parameters.AddWithValue("@AGENDAMEDICOID", horario.AgendaMedicoId);
                
                var idGerado = (int)comd.ExecuteScalar();
                
                trans.Commit();

                return idGerado;
            }
        }
        
        /// <summary>
        /// Busca as agendas do médico pelo seu ID
        /// </summary>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedico(int idMedico)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = AgendaMedicoSQLScript.BuscaAgendasDoMedicoPorID;

                var param = new DynamicParameters();

                param.Add("@MEDICOID", idMedico, DbType.Int32, ParameterDirection.Input);

                var result = conn.Query<AgendaMedicoMes>(sql, param, commandTimeout: Timeout);

                if (result.Any()) 
                {
                    foreach (var agenda in result)
                    {
                        var sql2 = AgendaMedicoSQLScript.ObtenhaHorariosDaAgendaPeloIdAgenda;

                        var param2 = new DynamicParameters();

                        param2.Add("@AGENDAMEDICOID", agenda.Id, DbType.Int32, ParameterDirection.Input);

                        var horaios = conn.Query<AgendaMedicoDia>(sql2, param2, commandTimeout: Timeout);

                        if (horaios.Any()) 
                        {
                            agenda.DiasDaAgenda = horaios.ToList();
                        }
                    }
                }

                return result ?? new List<AgendaMedicoMes>();
            }
        }

        /// <summary>
        /// Busca as agendas dos médicos
        /// </summary>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDosMedicos(int Limite)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = AgendaMedicoSQLScript.BuscaAgendasDosMedicos;

                var param = new DynamicParameters();

                param.Add("@LIMITE", Limite, DbType.Int32, ParameterDirection.Input);

                var result = conn.Query<AgendaMedicoMes>(sql, param, commandTimeout: Timeout);

                if (result.Any())
                {
                    foreach (var agenda in result)
                    {
                        var sql2 = AgendaMedicoSQLScript.ObtenhaHorariosDaAgendaPeloIdAgenda;

                        var param2 = new DynamicParameters();

                        param2.Add("@AGENDAMEDICOID", agenda.Id, DbType.Int32, ParameterDirection.Input);

                        var horaios = conn.Query<AgendaMedicoDia>(sql2, param2, commandTimeout: Timeout);

                        if (horaios.Any())
                        {
                            agenda.DiasDaAgenda = horaios.ToList();
                        }
                    }
                }

                return result ?? new List<AgendaMedicoMes>();
            }
        }

        /// <summary>
        /// Busca todos os horarios da agenda do médico
        /// </summary>
        /// <param name="IdAgendaMedico">Id da Agenda do Médico</param>
        /// <returns></returns>
        public IEnumerable<AgendaMedicoDia> BusqueTodosHorariosDaAgendaPorId(int IdAgendaMedico) 
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = AgendaMedicoSQLScript.ObtenhaHorariosDaAgendaPeloIdAgenda;

                var param = new DynamicParameters();

                param.Add("@AGENDAMEDICOID", IdAgendaMedico, DbType.Int32, ParameterDirection.Input);

                var result = conn.Query<AgendaMedicoDia>(sql, param, commandTimeout: Timeout);

                return result ?? new List<AgendaMedicoDia>();    
            }
        }

        /// <summary>
        /// Busca as agendas do médico pelo seu ID e ID da sua agenda
        /// </summary>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        public AgendaMedicoMes BusqueAgendaDoMedicoPorId(int idMedico, int IdAgenda)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = AgendaMedicoSQLScript.BuscaAgendaDoMedicoPorIdEhMedico;

                var param = new DynamicParameters();

                param.Add("@MEDICOID", idMedico, DbType.Int32, ParameterDirection.Input);
                param.Add("@ID", IdAgenda, DbType.Int32, ParameterDirection.Input);

                var result = conn.Query<AgendaMedicoMes>(sql, param, commandTimeout: Timeout);

                if (result.Any())
                {
                    foreach (var agenda in result)
                    {
                        var sql2 = AgendaMedicoSQLScript.ObtenhaHorariosDaAgendaPeloIdAgenda;

                        var param2 = new DynamicParameters();

                        param2.Add("@AGENDAMEDICOID", agenda.Id, DbType.Int32, ParameterDirection.Input);

                        var horaios = conn.Query<AgendaMedicoDia>(sql2, param2, commandTimeout: Timeout);

                        if (horaios.Any())
                        {
                            agenda.DiasDaAgenda = horaios.ToList();
                        }
                    }
                }

                return result.FirstOrDefault();
            }
        }

        /// <summary>
        /// Busca as agendas com base nos parametros do filtro
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="dia"></param>
        /// <param name="mesano"></param>
        /// <returns></returns>
        public IEnumerable<AgendaMedicoMes> BusqueTodasAgendasDoMedicoPorIdEhDiaEhMes(int idMedico, int dia, string mesano) 
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = AgendaMedicoSQLScript.BuscaAgendasDoMedicoPorIdEhDiaEhMes;

                var param = new DynamicParameters();

                param.Add("@MEDICOID", idMedico, DbType.Int32, ParameterDirection.Input);
                param.Add("@DIA", dia, DbType.Int32, ParameterDirection.Input);
                param.Add("@MESANO", mesano, DbType.String, ParameterDirection.Input);

                var result = conn.Query<AgendaMedicoMes>(sql, param, commandTimeout: Timeout);

                if (result.Any())
                {
                    foreach (var agenda in result)
                    {
                        var sql2 = AgendaMedicoSQLScript.ObtenhaHorariosDaAgendaPeloIdAgenda;

                        var param2 = new DynamicParameters();

                        param2.Add("@AGENDAMEDICOID", agenda.Id, DbType.Int32, ParameterDirection.Input);

                        var horaios = conn.Query<AgendaMedicoDia>(sql2, param2, commandTimeout: Timeout);

                        if (horaios.Any())
                        {
                            agenda.DiasDaAgenda = horaios.ToList();
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Atualiza a disponibilidade da agenda
        /// </summary>
        /// <param name="IdAgenda"></param>
        /// <param name="disponibilidade"></param>
        /// <returns></returns>
        public AgendaMedicoMes AtualizeDisponibilidadeAgendaMedicoPorId(int IdAgenda, string disponibilidade)
        {
            int idMedico = 0;

            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.AtualizeAgendaMedicoPorId;

                comd.Parameters.AddWithValue("@ID", IdAgenda);
                comd.Parameters.AddWithValue("@DIADISPONIVEL", disponibilidade);

                var linhasafetadas = (int)comd.ExecuteNonQuery();

                if (linhasafetadas != 0)
                {
                    comd.Parameters.Clear();
                    comd.CommandText = "SELECT MEDICOID FROM AGENDA_MEDICO_MES WITH (NOLOCK) WHERE ID IN (SELECT AGENDAMEDICOID FROM AGENDA_MEDICO_DIA WHERE ID = @ID)";
                    comd.Parameters.AddWithValue("@ID", IdAgenda);
                    var reader = comd.ExecuteReader();
                    while (reader.Read())
                    {
                        idMedico = reader.GetInt32(0);
                    }
                    reader.Close();
                }

                trans.Commit();
            }

            return BusqueAgendaDoMedicoPorId(idMedico, IdAgenda);
        }

        /// <summary>
        /// Atualiza um horario de determinada agenda e medico para um paciente
        /// </summary>
        /// <param name="idHorario"></param>
        /// <param name="IdAgendaMedico"></param>
        /// <param name="IdPaciente"></param>
        /// <param name="disponibilidade"></param>
        /// <returns></returns>
        public int AtualizeHorarioDaAgendaComPaciente(int idHorario, int IdAgendaMedico, int IdPaciente, string disponibilidade, byte[] versaolinha)
        {
            ulong ValorDaLinha = 
            ((ulong)versaolinha[0] << 56)
            | ((ulong)versaolinha[1] << 48)
            | ((ulong)versaolinha[2] << 40)
            | ((ulong)versaolinha[3] << 32)
            | ((ulong)versaolinha[4] << 24)
            | ((ulong)versaolinha[5] << 16)
            | ((ulong)versaolinha[6] << 8)
            | versaolinha[7];

            var versaoAtual = this.ObtenhaAhVersaoDaLinhaDoHorario(idHorario);

            ulong ValorDaLinhaAtual = 
           ((ulong)versaoAtual[0] << 56)
           | ((ulong)versaoAtual[1] << 48)
           | ((ulong)versaoAtual[2] << 40)
           | ((ulong)versaoAtual[3] << 32)
           | ((ulong)versaoAtual[4] << 24)
           | ((ulong)versaoAtual[5] << 16)
           | ((ulong)versaoAtual[6] << 8)
           | versaoAtual[7];

            if (ValorDaLinhaAtual != ValorDaLinha)
                throw new Exception("Horario já agendado por outro paciente.");

            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.AtualizeHorarioComPasciente;

                comd.Parameters.AddWithValue("@ID", idHorario);
                comd.Parameters.AddWithValue("@PACIENTEID", IdPaciente);
                comd.Parameters.AddWithValue("@AGENDAMEDICOID", IdAgendaMedico);
                comd.Parameters.AddWithValue("@HORARIODISPONIVEL", disponibilidade);

                var linhasafetadas = (int)comd.ExecuteNonQuery();

                trans.Commit();

                return linhasafetadas;
            }
        }

        /// <summary>
        /// Libere horario da agenda do médico para DISPONIVEL
        /// </summary>
        /// <param name="idHorario"></param>
        /// <param name="IdAgendaMedico"></param>
        /// <returns></returns>
        public int AtualizeLibereHorarioDaAgenda(int idHorario, int IdAgendaMedico)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.AtualizeLibereHorario;

                comd.Parameters.AddWithValue("@ID", idHorario);
                comd.Parameters.AddWithValue("@AGENDAMEDICOID", IdAgendaMedico);

                var linhasafetadas = (int)comd.ExecuteNonQuery();

                trans.Commit();

                return linhasafetadas;
            }
        }

        /// <summary>
        /// Atualiza o horario da agenda do médico conforme parametro
        /// </summary>
        /// <param name="idHorario"></param>
        /// <param name="IdAgendaMedico"></param>
        /// <returns></returns>
        public int AtualizeHorarioDaAgenda(int idHorario, int IdAgendaMedico, string horario)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.AtualizeHorario;

                comd.Parameters.AddWithValue("@ID", idHorario);
                comd.Parameters.AddWithValue("@AGENDAMEDICOID", IdAgendaMedico);
                comd.Parameters.AddWithValue("@HORARIO", horario);

                var linhasafetadas = (int)comd.ExecuteNonQuery();

                trans.Commit();

                return linhasafetadas;
            }
        }

        /// <summary>
        /// Remove um horario relacionado a agenda
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public int RemovaHorarioDaAgenda(int idHorario, int idAgendaMedico)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.DeleteHorario;

                comd.Parameters.AddWithValue("@ID", idHorario);
                comd.Parameters.AddWithValue("@AGENDAMEDICOID", idAgendaMedico);

                var linhasafetadas = (int)comd.ExecuteNonQuery();

                trans.Commit();

                return linhasafetadas;
            }
        }

        /// <summary>
        /// Remove uma agenda e seus horarios relacionados
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public int RemovaAgendaEhHorarioDaAgenda(int idAgendaMedico)
        {
            int linhasafetadas = 0;

            var listaDeIdsDeHorarios = BusqueTodosHorariosDaAgendaPorId(idAgendaMedico)?.Select(x => x.Id).ToList();

            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                comd.Transaction = trans;
                
                try
                {
                    comd.CommandText = AgendaMedicoSQLScript.DeleteHorario;

                    //executando a exclusão dos horarios
                    if (listaDeIdsDeHorarios != null && listaDeIdsDeHorarios.Any())
                    {
                        foreach (var id in listaDeIdsDeHorarios)
                        {
                            comd.Parameters.Clear();
                            comd.Parameters.AddWithValue("@ID", id);
                            comd.Parameters.AddWithValue("@AGENDAMEDICOID", idAgendaMedico);
                            linhasafetadas += (int)comd.ExecuteNonQuery();
                        }
                    }

                    comd.Parameters.Clear();
                    comd.CommandText = AgendaMedicoSQLScript.DeleteAgenda;
                    comd.Parameters.AddWithValue("@ID", idAgendaMedico);
                    linhasafetadas += (int)comd.ExecuteNonQuery();

                    trans.Commit();

                    return linhasafetadas;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return 0;
                }                
            }
        }

        /// <summary>
        /// Obtem a versao da linha do horario informado
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public byte[] ObtenhaAhVersaoDaLinhaDoHorario(int idHorario) 
        {
            byte[] bytes = [];

            using (var conn = new SqlConnection(ConnectionString))
            {
                SqlCommand comd = conn.CreateCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                comd.Transaction = trans;

                comd.CommandText = AgendaMedicoSQLScript.ObtenhaVersaoLinhaHorariosDaAgendaPeloId;

                comd.Parameters.AddWithValue("@ID", idHorario);
                var reader = comd.ExecuteReader();
               
                while (reader.Read())
                {
                    bytes = reader.GetFieldValue<byte[]>(0);
                }
                reader.Close();

                return bytes;
            }
        }
        
        public AgendaMedicoDia BusqueAgendaDiaDoMedicoPorId(int idHorario) 
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = AgendaMedicoSQLScript.ObtenhaHorarioDaAgendaPeloId;

                var param = new DynamicParameters();

                param.Add("@ID", idHorario, DbType.Int32, ParameterDirection.Input);

                return conn.Query<AgendaMedicoDia>(sql, param, commandTimeout: Timeout).FirstOrDefault();

            }
        }
    }
}
