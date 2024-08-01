using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Models;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.UseCase
{
    public class MedicoUseCase : IMedicoUseCase
    {
        private readonly IMedicoGateway _medicoGateway;
        private readonly IAgendaGateway _agendaGateway;
        public MedicoUseCase(IMedicoGateway medicoGateway, IAgendaGateway agendaGateway)
        {
            _medicoGateway = medicoGateway;
            _agendaGateway = agendaGateway;
        }

        public Medico AltereMedico(MedicoAlterarDAO medicoAlterarDAO)
        {
            var novoMedico = new Medico(medicoAlterarDAO);

            return _medicoGateway.Alterar(novoMedico);
        }

        public string AutentiqueMedico(MedicoDAO medico)
        {
            var medicoAut = new Medico(medico.Email, medico.Senha);

            var token = _medicoGateway.GerarToken(medicoAut);

            return token;
        }

        public void CadastreNovoMedico(MedicoDAO medico)
        {
            var novoMedico = new Medico(medico);

            _medicoGateway.Criar(novoMedico);
        }

        public void ExcluaMedico(int IdMedico)
        {
            var medico = _agendaGateway.BusqueTodasAgendasDoMedico(IdMedico);
            
            if (!medico.Any())
                _medicoGateway.Excluir(IdMedico);
            else
                throw new Exception(MensagensValidacoes.Medico_RelacaoAgenda);
        }
    }
}
