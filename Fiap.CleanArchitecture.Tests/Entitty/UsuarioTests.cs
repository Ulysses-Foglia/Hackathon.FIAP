using Bogus;
using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Entities;


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
            usuarioDAO.Nome = "is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains("Nome deve possuir no máximo 100 caracteres!", domainException.Message);

        }

        [Fact]
        public void Usuario_Validar_Email_Formato()
        {         

            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do email Invalido para teste
            usuarioDAO.Email = "is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains("Email no formato incorreto!", domainException.Message);

        }


        [Fact]
        public void Usuario_Validar_Senha_Tamanho()
        {

            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do senha Invalido para teste
            usuarioDAO.Senha = "is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains("Senha deve possuir entre 6 e 20 caracteres!", domainException.Message);

        }

        [Fact]
        public void Usuario_Validar_Papel_Formato()
        {

            UsuarioDAO usuarioDAO = _faker.Generate();

            //Dado do papel Invalido para teste
            usuarioDAO.Papel = "is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Usuario(usuarioDAO));

            Assert.Contains("Papel inválido! Verifique o formato correto.", domainException.Message);

        }

    }
}
