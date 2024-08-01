using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using Fiap.CleanArchitecture.UseCase;
using Fiap.CleanArchitecture.Presenter;

namespace Fiap.CleanArchitecture.Controller
{
    public class MedicoControlador : IMedicoControlador
    {
        private readonly IMedicoGateway _medicoGateway;
        private readonly IDatabaseClient _databaseClient;
        private readonly IMedicoUseCase _medicoUseCase;
        private readonly IAgendaGateway _agendaGateway;
        public MedicoControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _medicoGateway = new MedicoGateway(_databaseClient);   
            _agendaGateway = new AgendaGateway(_databaseClient);
            _medicoUseCase = new MedicoUseCase(_medicoGateway, _agendaGateway);
        }

        public string GerarToken(MedicoDAO medicoDAO)
        {
            return _medicoUseCase.AutentiqueMedico(medicoDAO);
        }

        public string BuscarTodos()
        {
            var medicos = _medicoGateway.BuscarTodos();

            return MedicoPresenter.ToJson(medicos);
        }

        public string BuscarMedicosDisponibilidade()
        {
            var medicos = _medicoGateway.BuscarMedicosDisponibilidade();

            return MedicoPresenter.ToJson(medicos);
        }

        public string BuscarPorId(int id)
        {
            var medico = _medicoGateway.BuscarPorId(id);

            return MedicoPresenter.ToJson(medico);
        }

        public void Criar(MedicoDAO medicoDAO)
        {
            _medicoUseCase.CadastreNovoMedico(medicoDAO);
        }

        public string Alterar(MedicoAlterarDAO medicoAlterarDAO)
        {
            var novoMedico = _medicoUseCase.AltereMedico(medicoAlterarDAO);

            return MedicoPresenter.ToJson(novoMedico);
        }

        public void Excluir(int id)
        {
            _medicoUseCase.ExcluaMedico(id);
        }
    }
}
