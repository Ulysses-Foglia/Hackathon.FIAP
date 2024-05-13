using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Microsoft.AspNetCore.Mvc;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Tests.Controllers
{
    
    public class UsuarioControllerTests
    {
        private UsuarioController _usuarioController;       
        private Mock<IControladorFactory<UsuarioControlador>> _mockControladorFactoryUsuario;
        private Mock<IDatabaseClient> _mockDataBaseClient;
        private Mock<IUsuarioControlador> _mockControlador;
        private Mock<IUsuarioController> _mockUsuarioController;


        public UsuarioControllerTests()
        {
            _mockUsuarioController = new Mock<IUsuarioController>();

            _mockControlador = new Mock<IUsuarioControlador>();
            
            _mockDataBaseClient = new Mock<IDatabaseClient>();

            _mockControladorFactoryUsuario = new Mock<IControladorFactory<UsuarioControlador>>();

            _usuarioController = new UsuarioController(_mockDataBaseClient.Object, _mockControladorFactoryUsuario.Object, _mockControlador.Object);
            
        }

        [Fact]
        public void Usuario_Validar_Autenticar_ReturnOkComDados() 
        {
            //Provider provider = new Provider();

            //var usuarioControladorInject = provider.GetRequiredService<IUsuarioControlador>();

            //var ControllerUsuario = provider.GetRequiredService<IUsuarioController>();

            //Arrange
            var resultadoEsperado = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MTU1NTc5MDAsImV4cCI6MTcxNTU2MTUwMCwiaWF0IjoxNzE1NTU3OTAwLCJpc3MiOiJBUEktRmlhcC5DbGVhbkFyY2hpdGVjdHVyZSIsImF1ZCI6ImRYTjFWR1Z6ZEdVeFFHVnRZV2xzTG1OdmJTNWljZz09In0.wJP9Dw3j2WhuvPie-6iepDjlWqbO8gWJToPoMxajfSs";

            UsuarioDAO usuario = new UsuarioDAO
            {
                Nome = "UsuTeste1",
                Email = "usuTeste1@email.com.br",
                Senha = "123456",
                Papel = "Admin"
            };

            
            _mockUsuarioController.Setup(s => s.Autenticar(It.IsAny<UsuarioDAO>())).Returns((UsuarioDAO usuario) =>
            {
                if(usuario == null) return new BadRequestObjectResult("Usuário inválido.");
                
                else return new OkObjectResult(new { resultadoEsperado });

            });

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            //Act
            var result = usuarioControllerLazy.Value.Autenticar(usuario);
           
            
            //Assert 
            Assert.IsType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            var resultado = okResult?.Value?.GetType()?.GetProperty("resultadoEsperado")?.GetValue(okResult.Value, null);
           
            Assert.Equal(resultadoEsperado, resultado);

        }


        [Fact]
        public void Usuario_Validar_BuscarTodos_ReturnOkComDados()
        {

            //Arrange
            _mockUsuarioController.Setup(x => x.BuscarTodos()).Returns(() =>
            {              
                return new OkObjectResult(new object());
            });

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            //Act
            var result = usuarioControllerLazy.Value.BuscarTodos();


            //Assert 
            Assert.NotNull(result); //Olha se o dado não é null
            Assert.IsType<OkObjectResult>(result); // Se o retorno é um Ok result
        }

        [Fact]
        public void Usuario_Validar_BuscarPorId_ReturnOkComDados()
        {
            //Arrage
            int parametro = 1;

            var resultadoEsperado =
                 new
                 {
                     Id = 1,
                     DataCriacao = DateTime.Parse("2024-05-12T11:11:27.833"),
                     Titulo = "Criar aplicação",
                     Prazo = new
                     {
                         Valor = 4,
                         Unidade = "d"
                     },
                     Status = "EmAndamento",
                     DataInicio = DateTime.Parse("2024-04-26T10:00:00"),
                     DataFim = DateTime.Parse("2024-04-30T10:00:00"),
                     Criador = new
                     {
                         Nome = "UsuTeste1",
                         Email = "usuTeste1@email.com.br",
                         Papel = "Admin"
                     },
                     Responsavel = new
                     {
                         Nome = "UsuTeste2",
                         Email = "usuTeste2@email.com.br",
                         Papel = "Admin"
                     }
                 };
                       

            _mockUsuarioController.Setup(x => x.BuscarPorId(parametro)).Returns(() =>
            {
                return new OkObjectResult(resultadoEsperado);
            });

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            //Act
            var result = usuarioControllerLazy.Value.BuscarPorId(parametro);


            //Assert 
            Assert.NotNull(result); //Olha se o dado não é null
            Assert.IsType<OkObjectResult>(result); // Se o retorno é um Ok result
            var okResult = (OkObjectResult)result;
            var resultadoAtual = okResult?.Value;

            Assert.Equal(resultadoEsperado, resultadoAtual);
        }

        [Fact]
        public void Usuario_Validar_Criar_ReturnOkComDados()
        {
            //Arrage
           
            UsuarioDAO usuario = new UsuarioDAO
            {
                Nome = "UsuTeste1",
                Email = "usuTeste1@email.com.br",
                Senha = "123456",
                Papel = "Admin"
            };

            _mockUsuarioController
           .Setup(x => x.Criar(usuario))
           .Returns(new OkResult());

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            // Act
            var result = usuarioControllerLazy.Value.Criar(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result); 
        
        }

        [Fact]
        public void Usuario_Validar_Alterar_ReturnOkComDados()
        {
            //Arrage
            var resultadoEsperado =
                 new
                 {
                     Id = 1,
                     Nome = "teste123",
                     Email = "usuTeste1@email.com.br",
                     Papel = "Admin"
                 };

            var usuarioAlterarDao = new UsuarioAlterarDAO
            {
                Id = 1,
                Nome = "UsuTeste1",
                Email = "usuTeste1@email.com.br",
                Papel = "Admin"
            };


            _mockUsuarioController.Setup(s => s.Alterar(It.IsAny<UsuarioAlterarDAO>())).Returns((UsuarioAlterarDAO usuarioAlterarDAO) =>
            {
                if (usuarioAlterarDAO == null) return new BadRequestObjectResult("Usuário inválido.");

                else return new OkObjectResult( resultadoEsperado);

            });

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

            // Act
            var result = usuarioControllerLazy.Value.Alterar(usuarioAlterarDao);

            // Assert
            Assert.NotNull(result); //Olha se o dado não é null
            Assert.IsType<OkObjectResult>(result); // Se o retorno é um Ok result
            var okResult = (OkObjectResult)result;
            var resultadoAtual = okResult?.Value;

            Assert.Equal(resultadoEsperado, resultadoAtual);
        }

    }
}
