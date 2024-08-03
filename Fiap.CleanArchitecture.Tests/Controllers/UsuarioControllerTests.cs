using Bogus;
using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Tests.Controllers
{
    public class UsuarioControllerTests
    {
        private UsuarioController _controller;
        private Mock<IUsuarioControlador> _mockUsuarioControlador;
        private Mock<IDatabaseClient> _mockDatabaseClient;
        private Provider _provider;
        private readonly Faker<UsuarioDAO> _fakerUsuario;
        private Mock<IUsuarioController> _mockUsuarioController;

        public UsuarioControllerTests()
        {
            _mockUsuarioController = new Mock<IUsuarioController>();
            _mockUsuarioControlador = new Mock<IUsuarioControlador>();
            _mockDatabaseClient = new Mock<IDatabaseClient>();
            _controller = new UsuarioController(_mockDatabaseClient.Object);
            _provider = new Provider();
            _fakerUsuario = RetornarFakerUsuario();
        }
                

        [Fact]
        public void Usuario_Validar_BuscarTodos_ReturnOkComDados()
        {
            //Arrange
            var retorno = _provider.GetRequiredService<IUsuarioController>().BuscarTodos();

            _mockUsuarioController.Setup(x => x.BuscarTodos()).Returns(() =>
            {
                return retorno;
            });

            var usuarioControllerLazy = 
                new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            //Act
            var result = usuarioControllerLazy.Value.BuscarTodos();

            //Assert 
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
             

        [Fact]
        public void Usuario_Validar_Criar_ReturnOkComDados()
        {
            //Arrage
            UsuarioDAO usuario = _fakerUsuario.Generate();

            _mockUsuarioControlador.Setup(x => x.Criar(usuario));

            // Act
            var result = _controller.Criar(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

                
        public Faker<UsuarioDAO> RetornarFakerUsuario() => new Faker<UsuarioDAO>()
            .RuleFor(u => u.Nome, f => f.Name.FirstName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Nome))
            .RuleFor(u => u.Senha, f => f.Internet.Password())
            .RuleFor(u => u.Papel, f => f.PickRandom(TipoPapel.Admin.ToString(), TipoPapel.Paciente.ToString()));
    }
}
