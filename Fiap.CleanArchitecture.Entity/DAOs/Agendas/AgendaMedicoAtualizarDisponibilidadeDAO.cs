// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoAtualizarDisponibilidadeDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoAtualizarDisponibilidadeDAO
    {
        public AgendaMedicoAtualizarDisponibilidadeDAO()
        {
            
        }
        public int AgendaId { get; set; }
        public string Disponibilidade { get; set; }

        public void ValideEntradaDeUsuario() 
        {
            if (!IdValido(this.AgendaId))
                throw new Exception("O id tem de ser informado");

            if (!DiaDisponivelValido(this.Disponibilidade))
                throw new Exception(MensagensValidacoes.Agenda_Mes_DiaDisponivel);
        }

        private bool IdValido(int Id) => Id != 0;
        private bool DiaDisponivelValido(string diaDisponivel) => diaDisponivel != null && diaDisponivel.Equals(DiaDisponivelEnum.DISPONIVEL.ToString()) || diaDisponivel.Equals(DiaDisponivelEnum.INDISPONIVEL.ToString());
    }
}
