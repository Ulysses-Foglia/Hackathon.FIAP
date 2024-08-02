// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoDiaDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoDiaDAO
    {
        public AgendaMedicoDiaDAO(){}
        
        public int Id { get; set; }
        public int AgendaMedicoId { get; set; }
        public string HorarioDisponivel { get; set; }
        public string Horario { get; set; }
        public int PacienteId { get; set; }
    }
}
