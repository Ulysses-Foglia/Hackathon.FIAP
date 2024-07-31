// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaPresenter.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Presenter
{
    public class AgendaPresenter
    {

        public static string ToJson(IEnumerable<AgendaMedicoMes> agendas) 
        {
            var listaNova = new List<AgendaMedicoMesDAO>();
            foreach (var item in agendas)
            {
                listaNova.Add(item.ConvertaEmDAO());
            }
            return JsonConvert.SerializeObject(listaNova);
        }

    }
}
