using Bogus;
using Bogus.DataSets;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;


namespace Fiap.CleanArchitecture.Tests.Entitty
{
    public class TarefaTests
    {
        private readonly Faker<TarefaDAO> _faker; 
        private readonly Faker<TarefaAlterarDAO> _fakerAlteracao; 
        public TarefaTests()
        {
            _faker = new Faker<TarefaDAO>()
             .RuleFor(t => t.Titulo, f => f.Lorem.Word())
             .RuleFor(t => t.PrazoValor, f => f.Random.Number(1, 30)) 
             .RuleFor(t => t.PrazoUnidade, f => f.PickRandom("d", "m", "h", "M", "S")) 
             .RuleFor(t => t.Status, f => f.PickRandom("Pendente", "Atribuida", "EmAndamento", "PendenteAprovacao", "Concluida")) 
             .RuleFor(t => t.CriadorId, f => f.Random.Number(1, 100));

            _fakerAlteracao = new Faker<TarefaAlterarDAO>()
            .RuleFor(t => t.Id, f => f.Random.Number(1, 1000))
            .RuleFor(t => t.Titulo, f => f.Lorem.Word())
            .RuleFor(t => t.PrazoValor, f => f.Random.Number(1, 30))
            .RuleFor(t => t.PrazoUnidade, f => f.PickRandom("d", "m", "h", "M", "S"))
            .RuleFor(t => t.Status, f => f.PickRandom("Pendente", "Atribuida", "EmAndamento", "PendenteAprovacao", "Concluida"))
            .RuleFor(t => t.DataInicio, f => f.Date.Past().ToString("yyyy-MM-dd"))
            .RuleFor(t => t.DataFim, f => f.Date.Future().ToString("yyyy-MM-dd"))
            .RuleFor(t => t.ResponsavelId, f => f.Random.Number(1, 100));
           
        }

        [Fact]
        public void Tarefa_Validar_Titulo_Tamanho()
        {

            TarefaDAO tarefaDAO = _faker.Generate();
            tarefaDAO.Titulo = "Documentar API -  is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaDAO));

            Assert.Contains("Titulo deve possuir no máximo 100 caracteres!", domainException.Message);

        }

        [Fact]
        public void Tarefa_Validar_Prazo_Formato()
        {          

            TarefaDAO tarefaDAO = _faker.Generate();

            tarefaDAO.PrazoValor = 0;
            tarefaDAO.PrazoUnidade = "v";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaDAO));

            Assert.Contains("Prazo inválido! Verifique o formato correto.", domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_Status_Formato()
        {

            TarefaDAO tarefaDAO = _faker.Generate();
            tarefaDAO.Status = "Invalido";
         

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaDAO));

            Assert.Contains("Status inválido! Verifique o formato correto.", domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_DataInicio_Formato()
        {

            TarefaAlterarDAO tarefaAlterarDAO = _fakerAlteracao.Generate();
            tarefaAlterarDAO.DataInicio = "2024-05-15 11:31:01 testeInvalido";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaAlterarDAO));

            Assert.Contains("Data Inicio está em formato incorreto!", domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_DataFim_Formato()
        {         

            TarefaAlterarDAO tarefaAlterarDAO = _fakerAlteracao.Generate();
            tarefaAlterarDAO.DataFim = "2024-05-15 11:31:01 testeInvalido";


            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaAlterarDAO));

            Assert.Contains("Data Fim está em formato incorreto!", domainException.Message);
        }


    }
}
