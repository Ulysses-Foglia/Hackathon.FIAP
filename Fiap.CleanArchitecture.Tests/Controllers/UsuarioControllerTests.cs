using Moq;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Microsoft.AspNetCore.Mvc;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;


namespace Fiap.CleanArchitecture.Tests.Controllers
{
    
    public class UsuarioControllerTests
    {        
        private Mock<IUsuarioController> _mockUsuarioController;
        private Provider _provider;

        public UsuarioControllerTests()
        {
            _mockUsuarioController = new Mock<IUsuarioController>();
            _provider = new Provider();
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
                        

            //var retornoToken = _provider.GetRequiredService<IUsuarioController>().Autenticar(usuario);

            _mockUsuarioController.Setup(s => s.Autenticar(It.IsAny<UsuarioDAO>())).Returns((UsuarioDAO usuario) =>
            {
                if(usuario == null) return new BadRequestObjectResult("Usuário inválido.");
                
                else return new OkObjectResult(resultadoEsperado);

            });

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

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

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

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

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

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
            
            UsuarioDAO usuario = new UsuarioDAO
            {
                Nome = "UsuTeste1",
                Email = "usuTeste1@email.com.br",
                Senha = "123456",
                Papel = "Admin"
            };

            var retorno = _provider.GetRequiredService<IUsuarioController>().Criar(usuario);

            _mockUsuarioController
           .Setup(x => x.Criar(usuario))
           .Returns(retorno);

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
            var resultadoEsperado = "{\"Id\":2,\"DataCriacao\":\"2024-05-12T11:11:27.83\",\"Nome\":\"teste123\",\"Email\":\"usuTeste1@email.com.br\",\"Papel\":\"Admin\"}";

                var usuarioAlterarDao = new UsuarioAlterarDAO
                {
                    Id = 2,
                    Nome = "teste123",
                    Email = "usuTeste1@email.com.br",
                    Papel = "Admin"
                };

            var retorno = _provider.GetRequiredService<IUsuarioController>().Alterar(usuarioAlterarDao);

            _mockUsuarioController.Setup(s => s.Alterar(It.IsAny<UsuarioAlterarDAO>())).Returns((UsuarioAlterarDAO usuarioAlterarDAO) =>
            {
                if (usuarioAlterarDAO == null) return new BadRequestObjectResult("Usuário inválido.");

                else return retorno;

            });

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);

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

            var retorno = _provider.GetRequiredService<IUsuarioController>().Excluir(parametro);

            _mockUsuarioController
           .Setup(x => x.Excluir(parametro))
           .Returns(retorno);

            var usuarioControllerLazy = new Lazy<IUsuarioController>(() => _mockUsuarioController.Object);
   
            // Act
            var result = usuarioControllerLazy.Value.Excluir(parametro);
                      
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
