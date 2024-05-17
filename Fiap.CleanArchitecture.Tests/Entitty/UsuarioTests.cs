using Bogus;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Tests.Entitty
{
    public class UsuarioTests
    {
        private readonly Faker<UsuarioDAO> _faker;

        public UsuarioTests()
        {
            _faker = new Faker<UsuarioDAO>()
            .RuleFor(u => u.Nome, f => f.Name.FirstName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Nome))
            .RuleFor(u => u.Senha, f => f.Internet.Password())
            .RuleFor(u => u.Papel, f => f.PickRandom("Admin", "Comun"));
        }

        [Fact]
        public void Usuario_Validar_Nome_Tamanho()
        {
            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do nome Invalido para teste
            usuarioDAO.Nome = MensagensValidacoes.Tests_TextoExemplo;

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains(MensagensValidacoes.Usuario_Nome, domainException.Message);
        }

        [Fact]
        public void Usuario_Validar_Email_Formato()
        {
            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do email Invalido para teste
            usuarioDAO.Email = MensagensValidacoes.Tests_TextoExemplo;

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains(MensagensValidacoes.Usuario_Email, domainException.Message);
        }


        [Fact]
        public void Usuario_Validar_Senha_Tamanho()
        {
            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do senha Invalido para teste
            usuarioDAO.Senha = MensagensValidacoes.Tests_TextoExemplo;

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains(MensagensValidacoes.Usuario_Senha, domainException.Message);
        }

        [Fact]
        public void Usuario_Validar_Papel_Formato()
        {
            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do papel Invalido para teste
            usuarioDAO.Papel = MensagensValidacoes.Tests_TextoExemplo;

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains(MensagensValidacoes.Usuario_Papel, domainException.Message);
        }
    }
}
