using Bogus;
using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Agendas;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Tests.Controllers
{
    public class AgendaControllerTests
    {
        private AgendaController _controller;
        private Mock<IAgendaController> _mockAgendaController;
        private Provider _provider;
        private readonly Faker<AgendaMedicoMesDAO> _fakerAgendaMedicoMes;
        private readonly Faker<AgendaMedicoDiaDAO> _fakerAgendaMedicoDia;
        private readonly Faker<AgendaMedicoAgendarPacienteDAO> _fakerAgendaMedicoAgendarPaciente;
        private Mock<IDatabaseClient> _mockDatabaseClient;
        private readonly Faker<MedicoDAO> _fakerMedico;
        private readonly AgendaSQLRepository _agentaSQLRepository;
        public AgendaControllerTests()
        {

            _mockAgendaController = new Mock<IAgendaController>();
            _provider = new Provider();
            _agentaSQLRepository = new AgendaSQLRepository(_provider.Configuration);
            _fakerMedico = RetornarFakerMedico();
            _fakerAgendaMedicoDia = RetornarFakerAgenaMedicoDia();
            _fakerAgendaMedicoMes = RetornarFakerAgenaMedicoMes(_fakerAgendaMedicoDia, _fakerMedico);
            _fakerAgendaMedicoAgendarPaciente = RetornarFakerAgendaMedicoAgendarPaciente();
            _mockDatabaseClient = new Mock<IDatabaseClient>();
            _controller = new AgendaController(_mockDatabaseClient.Object, _provider.Configuration);
        }

        [Fact]
        public void Agenda_Validar_CriacaoAgendaMedico_returnoOk()
        {
            var retorno = _fakerAgendaMedicoMes.Generate();

            _mockAgendaController.Setup(x => x.CriarAgendaMedico(retorno));
            var retornoProvider = _provider.GetRequiredService<IAgendaController>().CriarAgendaMedico(retorno);

            Assert.NotNull(retornoProvider);
            Assert.IsType<OkResult>(retornoProvider);
        }

        [Fact]
        public void CriarHorarioAgendaMedico()
        {
            var retorno = _fakerAgendaMedicoDia.Generate();

            var agendaMedico = _agentaSQLRepository.BusqueTodasAgendasDosMedicos(3).FirstOrDefault();

            retorno.AgendaMedicoId = !(agendaMedico is null) ? agendaMedico.Id : throw new InvalidOperationException("Nenhuma agenda médica foi encontrada.");

            _mockAgendaController.Setup(x => x.CriarHorarioAgendaMedico(retorno));

            var retornoProvider = (OkObjectResult)_provider.GetRequiredService<IAgendaController>().CriarHorarioAgendaMedico(retorno);

            Assert.NotNull(retornoProvider);
            Assert.IsType<OkObjectResult>(retornoProvider);
            Assert.Equal(200, retornoProvider.StatusCode);
        }
  
        [Fact]
        public void AgendePaciente()
        {
            var retorno = _fakerAgendaMedicoAgendarPaciente.Generate();

            var agendaMedico = _agentaSQLRepository.BusqueTodasAgendasDosMedicos(7).OrderByDescending(x => x.Id).FirstOrDefault();
            var agendaDia = _agentaSQLRepository.BusqueTodosHorariosDaAgendaPorId(agendaMedico.Id).OrderByDescending(x=>x.Id).FirstOrDefault();    

            retorno.IdAgendaMedico = !(agendaMedico is null) ? agendaMedico.Id : throw new InvalidOperationException("Nenhuma agenda médica foi encontrada.");
            retorno.IdHorario = !(agendaDia is null) ? agendaDia.Id : throw new InvalidOperationException("Nenhuma agenda médica foi encontrada."); 

            _mockAgendaController.Setup(x => x.AgendePaciente(retorno));

            var retornoProvider = (OkObjectResult)_provider.GetRequiredService<IAgendaController>().AgendePaciente(retorno);

            Assert.NotNull(retornoProvider);
            Assert.IsType<OkObjectResult>(retornoProvider);
            Assert.Equal(200, retornoProvider.StatusCode);
        }

      

        public static Faker<AgendaMedicoDiaDAO> RetornarFakerAgenaMedicoDia() => new Faker<AgendaMedicoDiaDAO>()
            .RuleFor(x => x.AgendaMedicoId, item => item.Random.Int(1, 1))
            .RuleFor(x => x.Id, item => item.Random.Int(1, 1))
            .RuleFor(x => x.HorarioDisponivel, item => item.PickRandom(new[] { "DISPONIVEL", "INDISPONIVEL" }))
            .RuleFor(x => x.Horario, item => item.Date.Future().ToString("HH:mm"))
            .RuleFor(x => x.PacienteId, item => item.Random.Int(1, 1));

        public static Faker<AgendaMedicoMesDAO> RetornarFakerAgenaMedicoMes(Faker<AgendaMedicoDiaDAO> fakerAgendaMedicoDia, Faker<MedicoDAO> medico) => new Faker<AgendaMedicoMesDAO>()
               .RuleFor(x => x.Id, item => item.Random.Int(1, 3))
                .RuleFor(x => x.MedicoId, item => item.Random.Int(1, 3))
                .RuleFor(x => x.MesAno, item => item.Date.Past().ToString("MM/yyyy").Replace("/", ""))
                .RuleFor(x => x.Dia, item => item.Random.Int(1, 31))
                .RuleFor(x => x.DiaDisponivel, item => item.PickRandom(new[] { "DISPONIVEL", "INDISPONIVEL" }))
                .RuleFor(x => x.DiasDaAgenda, item => fakerAgendaMedicoDia.Generate(5).ToList());
        public static Faker<MedicoDAO> RetornarFakerMedico() => new Faker<MedicoDAO>()
           .RuleFor(x => x.Nome, item => item.Name.FirstName())
           .RuleFor(x => x.Email, (item, itemm) => item.Internet.Email(itemm.Nome))
           .RuleFor(x => x.Senha, item => item.Internet.Password())
           .RuleFor(x => x.Papel, _ => TipoPapel.Medico.ToString())
           .RuleFor(x => x.Crm, item => item.Random.Replace("######"));

        public static Faker<AgendaMedicoAgendarPacienteDAO> RetornarFakerAgendaMedicoAgendarPaciente() => new Faker<AgendaMedicoAgendarPacienteDAO>()
            .RuleFor(a => a.IdHorario, f => f.Random.Int(1, 1))
            .RuleFor(a => a.IdAgendaMedico, f => f.Random.Int(1, 1))
            .RuleFor(a => a.IdPaciente, f => f.Random.Int(1, 1));

    }
}
