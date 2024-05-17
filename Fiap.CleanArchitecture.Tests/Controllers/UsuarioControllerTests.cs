using Bogus;
using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
        public void Usuario_Validar_Autenticar_ReturnOkComDados()
        {
            //Arrange
            var resultadoEsperado = new { token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MTU2NDQ0NzksImV4cCI6MTcxNTY0ODA3OSwiaWF0IjoxNzE1NjQ0NDc5LCJpc3MiOiJBUEktRmlhcC5DbGVhbkFyY2hpdGVjdHVyZSIsImF1ZCI6ImRYTjFWR1Z6ZEdVeFFHVnRZV2xzTG1OdmJTNWljZz09In0.4ulEKcfcgvqNI1-6czZUQp5nrOl8D-p9uFgG9WgI5EU" };

            UsuarioDAO usuario = new UsuarioDAO
            {
                Nome = "UsuTeste1",
                Email = "usuTeste1@email.com.br",
                Senha = "123456",
                Papel = "Admin"
            };

            _mockUsuarioController.Setup(s => s.Autenticar(It.IsAny<UsuarioDAO>()))
                .Returns((UsuarioDAO usuario) =>
            {
                if (usuario == null)
                    return new BadRequestObjectResult(MensagensValidacoes.Tests_UsuarioInvalido);
                else
                    return new OkObjectResult(resultadoEsperado);
            });

            var usuarioControllerLazy =
                new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            //Act
            var result = usuarioControllerLazy.Value.Autenticar(usuario);

            //Assert 
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var token = okResult.Value;
            Assert.Equal(resultadoEsperado, token);
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
        public void Usuario_Validar_BuscarPorId_ReturnOkComDados()
        {
            //Arrage
            int parametro = 1;
            var resultadoEsperado = "{\"Id\":1,\"DataCriacao\":\"2024-05-12T11:11:27.83\",\"Nome\":\"UsuTeste1\",\"Email\":\"usuTeste1@email.com.br\",\"Papel\":\"Admin\"}";

            var retorno = _provider.GetRequiredService<IUsuarioController>().BuscarPorId(parametro);

            _mockUsuarioController.Setup(x => x.BuscarPorId(parametro)).Returns(() =>
            {
                return retorno;
            });

            var usuarioControllerLazy = 
                new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            //Act
            var result = usuarioControllerLazy.Value.BuscarPorId(parametro);

            //Assert 
            var okResult = Assert.IsType<OkObjectResult>(result)?.Value?.ToString();

            Assert.Equal(resultadoEsperado, okResult);
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

        [Fact]
        public void Usuario_Validar_Alterar_ReturnOkComDados()
        {
            //Arrage
            var resultadoEsperado = "{\"Id\":2,\"DataCriacao\":\"2024-05-12T11:11:27.83\",\"Nome\":\"teste123\",\"Email\":\"usuTeste1@email.com.br\",\"Papel\":\"Admin\"}";

            var usuarioAlterarDao = new UsuarioAlterarDAO
            {
                Id = 2,
                Nome = "teste123",
                Email = "usuTeste1@email.com.br",
                Papel = "Admin"
            };

            var retorno = _provider.GetRequiredService<IUsuarioController>().Alterar(usuarioAlterarDao);

            _mockUsuarioController.Setup(s => s.Alterar(It.IsAny<UsuarioAlterarDAO>()))
                .Returns((UsuarioAlterarDAO usuarioAlterarDAO) =>
            {
                if (usuarioAlterarDAO == null) 
                    return new BadRequestObjectResult(MensagensValidacoes.Tests_UsuarioInvalido);
                else 
                    return retorno;

            });

            var usuarioControllerLazy = 
                new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            // Act
            var result = usuarioControllerLazy.Value.Alterar(usuarioAlterarDao);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            var resultadoAtual = okResult?.Value;

            Assert.Equal(resultadoEsperado, resultadoAtual);
        }

        [Fact]
        public void Usuario_Validar_Excluir_ReturnOkComDados()
        {
            //Arrage
            var parametro = 4;

            _mockUsuarioControlador.Setup(x => x.Excluir(parametro));

            // Act
            var result = _controller.Excluir(parametro);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        public Faker<UsuarioDAO> RetornarFakerUsuario() => new Faker<UsuarioDAO>()
            .RuleFor(u => u.Nome, f => f.Name.FirstName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Nome))
            .RuleFor(u => u.Senha, f => f.Internet.Password())
            .RuleFor(u => u.Papel, f => f.PickRandom(TipoPapel.Admin.ToString(), TipoPapel.Comum.ToString()));
    }
}
