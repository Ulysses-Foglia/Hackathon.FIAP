// ------------------------------------------------------------------------------------------------------
// <copyright file="Medico.cs" company="TJ Sistemas">
// Copyright © TJ Sistemas. All rights reserved.
// TODOS OS DIREITOS RESERVADOS.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Fiap.CleanArchitecture.Entities;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Enums;
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

        public Medico(MedicoDAO medicoDAO) : base(medicoDAO) 
        {
            ValidarEntity(medicoDAO.Crm);

            if (!medicoDAO.Papel.Equals("Medico"))
                throw new Exception(MensagensValidacoes.Usuario_Papel_invalido_medico);

            if (!CrmValido(medicoDAO.Crm))
                throw new Exception(MensagensValidacoes.Usuario_Crm_Inavalido);
            
            this.Crm = medicoDAO.Crm;
        }

        public Medico(MedicoDAO medicoDAO, string crm, AgendaMedicoMes agenda) : base(medicoDAO)
        {
            ValidarEntity(crm, agenda);

            if (!medicoDAO.Papel.Equals("Medico"))
                throw new Exception(MensagensValidacoes.Usuario_Papel_invalido_medico);

            if (!CrmValido(crm))
                throw new Exception(MensagensValidacoes.Usuario_Crm_Inavalido);

            if (!AgendaValida(agenda))
                throw new Exception(MensagensValidacoes.Usuario_Agenda_Inavalido);

            this.Crm = crm;
        }


        public Medico(MedicoAlterarDAO medicoAlterarDAO) : base(medicoAlterarDAO)
        {
            if (!CrmValido(medicoAlterarDAO.Crm))
                throw new Exception(MensagensValidacoes.Usuario_Crm_Inavalido);

            this.Crm = medicoAlterarDAO.Crm;
        }

        private  bool CrmValido(string crm) => crm != null && crm != "" && crm.Length == 6;

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
