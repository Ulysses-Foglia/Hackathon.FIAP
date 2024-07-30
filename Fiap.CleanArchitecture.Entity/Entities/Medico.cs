// ------------------------------------------------------------------------------------------------------
// <copyright file="Medico.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entities;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Models;


namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Medico : Usuario
    {

        public  string Crm { get; set; }

        public int AgendaId { get; set; }

        public AgendaMedicoMes Agenda { get; set; }

        public Medico()
        {
            
        }
        public Medico(string email, string senha) : base(email, senha) { }

        public Medico(UsuarioDAO usuarioDAO, string crm) : base(usuarioDAO) 
        {
            ValidarEntity(crm);

            if (!usuarioDAO.Papel.Equals("Medico"))
                throw new Exception(MensagensValidacoes.Usuario_Papel_invalido_medico);    

            if (!CrmValido(crm))
                throw new Exception(MensagensValidacoes.Usuario_Crm_Inavalido);
            
            this.Crm = crm;
        }

        public Medico(UsuarioDAO usuarioDAO, string crm, AgendaMedicoMes agenda) : base(usuarioDAO)
        {
            ValidarEntity(crm, agenda);

            if (!usuarioDAO.Papel.Equals("Medico"))
                throw new Exception(MensagensValidacoes.Usuario_Papel_invalido_medico);

            if (!CrmValido(crm))
                throw new Exception(MensagensValidacoes.Usuario_Crm_Inavalido);

            if (!AgendaValida(agenda))
                throw new Exception(MensagensValidacoes.Usuario_Agenda_Inavalido);

            this.Crm = crm;
        }

        private  bool CrmValido(string crm) => crm != null && crm != "" && crm.Length == 7 && crm.Contains("/");

        private bool AgendaValida(AgendaMedicoMes agt) => agt != null && agt.DiasDaAgenda.Any();

        public void ValidarEntity(string crm)
        {
            AssertionConcern.AssertArgumentTrue(CrmValido(crm), MensagensValidacoes.Usuario_Crm_Inavalido);
        }

        public void ValidarEntity(string crm, AgendaMedicoMes agenda)
        {
            AssertionConcern.AssertArgumentTrue(CrmValido(crm), MensagensValidacoes.Usuario_Crm_Inavalido);
            AssertionConcern.AssertArgumentTrue(AgendaValida(agenda), MensagensValidacoes.Usuario_Agenda_Inavalido);
        }

    }
}
