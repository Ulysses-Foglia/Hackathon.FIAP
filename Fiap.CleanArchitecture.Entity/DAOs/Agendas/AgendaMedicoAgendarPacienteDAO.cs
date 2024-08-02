// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoAgendarPacienteDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entity.Enums;

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoAgendarPacienteDAO
    {
        public AgendaMedicoAgendarPacienteDAO()
        {
            
        }

        public int IdHorario { get; set; }
        public int IdAgendaMedico { get; set; }
        public int IdPaciente { get; set; }

        
        public void ValideEntradaDeUsuario()
        {
            if (!IdValido(this.IdHorario))
                throw new Exception("O idHorario tem de ser informado");
            if (!IdValido(this.IdAgendaMedico))
                throw new Exception("O idAgendaMedico tem de ser informado");
            if (!IdValido(this.IdPaciente))
                throw new Exception("O idPaciente tem de ser informado");
        }

        private bool IdValido(int Id) => Id != 0;
        private bool DiaDisponivelValido(string diaDisponivel) => diaDisponivel != null && diaDisponivel.Equals(DiaDisponivelEnum.DISPONIVEL.ToString()) || diaDisponivel.Equals(DiaDisponivelEnum.INDISPONIVEL.ToString());
    }
}
