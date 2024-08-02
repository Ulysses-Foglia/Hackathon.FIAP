using Bogus;
using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.CleanArchitecture.Tests.Controllers
{
    public class MedicoControllerTests 
    {
        private MedicoController _controller;
        private Mock<IMedicoController> _mockMedicoController;
        private Provider _provider;
        private readonly Faker<MedicoDAO> _fakerMedico;
        private Mock<IDatabaseClient> _mockDatabaseClient;
        
        public MedicoControllerTests()
        {
            _mockMedicoController = new Mock<IMedicoController>();
            _provider = new Provider();
            _fakerMedico = RetornarFakerMedico();
            _mockDatabaseClient = new Mock<IDatabaseClient>();
            _controller = new MedicoController(_mockDatabaseClient.Object);
        }

        [Fact]
        public void Medico_Validar_Autenticar_ReturnOkComDados()
        {
            //Arrange
            var resultadoEsperado = new { token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MTU2NDQ0NzksImV4cCI6MTcxNTY0ODA3OSwiaWF0IjoxNzE1NjQ0NDc5LCJpc3MiOiJBUEktRmlhcC5DbGVhbkFyY2hpdGVjdHVyZSIsImF1ZCI6ImRYTjFWR1Z6ZEdVeFFHVnRZV2xzTG1OdmJTNWljZz09In0.4ulEKcfcgvqNI1-6czZUQp5nrOl8D-p9uFgG9WgI5EU" };

            AutenticacaoModelDAO medico = new AutenticacaoModelDAO
            {

                Email = "usuTeste1@email.com.br",
                Senha = "123456",

            };

            _mockMedicoController.Setup(s => s.AutenticarMedico(It.IsAny<AutenticacaoModelDAO>()))
                .Returns((MedicoDAO medico) =>
                {
                    if (medico == null)
                        return new BadRequestObjectResult(MensagensValidacoes.Tests_UsuarioInvalido);
                    else
                        return new OkObjectResult(resultadoEsperado);
                });

            var medicoControllerLazy =
                new Lazy<IMedicoController>(() => _mockMedicoController.Object);

            //Act
            var result = medicoControllerLazy.Value.AutenticarMedico(medico);

            //Assert 
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var token = okResult.Value;
            Assert.Equal(resultadoEsperado, token);
        }

        [Fact]
        public void Medico_Validar_BuscarMedicosDisponibilidade_ReturnOkComDados()
        {
            //Arrange
            var retorno = _provider.GetRequiredService<IMedicoController>().BuscarMedicosDisponibilidade();

            _mockMedicoController.Setup(x => x.BuscarMedicosDisponibilidade()).Returns(() =>
            {
                return retorno;
            });

            var medicoControllerLazy =
                new Lazy<IMedicoController>(() => _mockMedicoController.Object);

            //Act
            var result = medicoControllerLazy.Value.BuscarMedicosDisponibilidade();

            //Assert 
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Medico_Validar_BuscarTodos_ReturnOkComDados()
        {
            //Arrange
            var retorno = _provider.GetRequiredService<IMedicoController>().BuscarTodos();

            _mockMedicoController.Setup(x => x.BuscarTodos()).Returns(() =>
            {
                return retorno;
            });

            var medicoControllerLazy =
                new Lazy<IMedicoController>(() => _mockMedicoController.Object);

            //Act
            var result = medicoControllerLazy.Value.BuscarTodos();

            //Assert 
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Criar()
        {
            //Arrage
            MedicoDAO medico = _fakerMedico.Generate();

            _mockMedicoController.Setup(x => x.Criar(medico));

            // Act
            var result = _controller.Criar(medico);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        public static Faker<MedicoDAO> RetornarFakerMedico() => new Faker<MedicoDAO>()
        .RuleFor(m => m.Nome, f => f.Name.FirstName())
        .RuleFor(m => m.Email, (f, m) => f.Internet.Email(m.Nome))
        .RuleFor(m => m.Senha, f => f.Internet.Password())
        .RuleFor(m => m.Papel, _ => TipoPapel.Medico.ToString())
        .RuleFor(m => m.Crm, f => f.Random.Replace("######"));
    }
}
