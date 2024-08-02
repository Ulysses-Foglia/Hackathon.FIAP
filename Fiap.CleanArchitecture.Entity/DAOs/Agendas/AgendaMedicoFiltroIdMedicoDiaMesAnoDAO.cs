// ------------------------------------------------------------------------------------------------------
// <copyright file="AgendaMedicoFiltroIdMedicoDiaMesAnoDAO.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Fiap.CleanArchitecture.Entity.DAOs.Agendas
{
    public class AgendaMedicoFiltroIdMedicoDiaMesAnoDAO
    {
        public AgendaMedicoFiltroIdMedicoDiaMesAnoDAO(){}

        public int IdMedico { get; set; }
        public int Dia { get; set; }
        public string MesAno { get; set; }

        public void ValideEntradaDoUsuario()
        {
            if (!IdValido(this.IdMedico))
                throw new Exception("O Id do Medico não foi informado.");
            if (!IdValido(this.Dia))
                throw new Exception("O Dia não foi informado.");
            if (!MesAnoValido(this.MesAno))
                throw new Exception("O Mes e Ano não está formatado corretamente, tente MMAAAA(010001)");
        }

        private bool IdValido(int Id) => Id != 0;
        private bool MesAnoValido(string mesAno) => mesAno != null && mesAno.Length > 0 && mesAno.Length <= 6;
    }
}
