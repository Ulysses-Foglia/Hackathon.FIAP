// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaPresenter.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Presenter
{
    public class AgendaPresenter
    {
        public  int Id { get; set; }
        public  int MedicoId { get; set; }

        public  string MesAno { get; set; }

        public  int Dia { get; set; }

        public  string DiaDisponivel { get; set; }

        public static string ToJson(IEnumerable<AgendaMedicoMes> agendas) 
        {
            var listaNova = new List<AgendaMedicoMesDAO>();
            foreach (var item in agendas)
            {
                listaNova.Add(item.ConvertaEmDAO());
            }
            return JsonConvert.SerializeObject(listaNova);
        }

        public static string ToJson(AgendaMedicoMes agenda)
        {
            var agendaPresenter = new AgendaPresenter()
            {
                Id = agenda.Id,
                MedicoId = agenda.MedicoId,
                MesAno = agenda.MesAno,
                Dia = agenda.Dia,
                DiaDisponivel = agenda.DiaDisponivel.ToString()
            };

            return JsonConvert.SerializeObject(agendaPresenter);
        }

    }
}
