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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories
{
    internal class AgendaSQLRepository : Repository
    {

        private readonly IConfiguration _configuration;

        public AgendaSQLRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


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

                        param2.Add("@AGENDAMEDICOID", idMedico, DbType.Int32, ParameterDirection.Input);

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

    }
}
