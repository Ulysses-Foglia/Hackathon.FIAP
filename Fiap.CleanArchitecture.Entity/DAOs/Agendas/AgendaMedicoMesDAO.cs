// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------


using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using System.Collections.Generic;

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoMesDAO
    {
        public int Id { get; set; }

        public int MedicoId { get; set; }

        public string MesAno { get; set; }

        public int Dia { get; set; }

        public string DiaDisponivel { get; set; }

        public ICollection<AgendaMedicoDiaDAO> DiasDaAgenda { get; set; }

        public ICollection<AgendaMedicoDia> ConvertaDiasDaAgendaEntity(bool EhNovoCadastro) 
        {
            ICollection <AgendaMedicoDia> _DiasDaAgenda = new List<AgendaMedicoDia>();
            if (DiasDaAgenda.Any()) 
            {
                foreach (var item in DiasDaAgenda)
                {
                    _DiasDaAgenda.Add(new AgendaMedicoDia(item, EhNovoCadastro));
                }
            }

            return _DiasDaAgenda;
        }

    }
}
