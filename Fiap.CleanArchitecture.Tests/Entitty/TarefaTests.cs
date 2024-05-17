using Bogus;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Tests.Entitty
{
    public class TarefaTests
    {
        private readonly Faker<TarefaDAO> _faker;
        private readonly Faker<TarefaAlterarDAO> _fakerAlteracao;

        public TarefaTests()
        {
            _faker = RetornarFakerDao();
            _fakerAlteracao = RetornarTarefaAlterarDAO();
        }

        [Fact]
        public void Tarefa_Validar_Titulo_Tamanho()
        {
            TarefaDAO tarefaDAO = _faker.Generate();
            tarefaDAO.Titulo = "Documentar API - " + MensagensValidacoes.Tests_TextoExemplo;

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaDAO));

            Assert.Contains(MensagensValidacoes.Tarefa_Titulo, domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_Prazo_Formato()
        {
            TarefaDAO tarefaDAO = _faker.Generate();

            tarefaDAO.PrazoValor = 0;
            tarefaDAO.PrazoUnidade = "v";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaDAO));

            Assert.Contains(MensagensValidacoes.Tarefa_Prazo, domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_Status_Formato()
        {
            TarefaDAO tarefaDAO = _faker.Generate();
            tarefaDAO.Status = "Invalido";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaDAO));

            Assert.Contains(MensagensValidacoes.Tarefa_Status, domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_DataInicio_Formato()
        {
            TarefaAlterarDAO tarefaAlterarDAO = _fakerAlteracao.Generate();
            tarefaAlterarDAO.DataInicio = "2024-05-15 11:31:01 testeInvalido";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaAlterarDAO));

            Assert.Contains(MensagensValidacoes.Tarefa_DataInicio, domainException.Message);
        }

        [Fact]
        public void Tarefa_Validar_DataFim_Formato()
        {
            TarefaAlterarDAO tarefaAlterarDAO = _fakerAlteracao.Generate();
            tarefaAlterarDAO.DataFim = "2024-05-15 11:31:01 testeInvalido";

            var domainException = Assert.ThrowsAny<DomainException>(() => new Tarefa(tarefaAlterarDAO));

            Assert.Contains(MensagensValidacoes.Tarefa_DataFim, domainException.Message);
        }

        public Faker<TarefaDAO> RetornarFakerDao() => new Faker<TarefaDAO>()
             .RuleFor(t => t.Titulo, f => f.Lorem.Word())
             .RuleFor(t => t.PrazoValor, f => f.Random.Number(1, 30))
             .RuleFor(t => t.PrazoUnidade, f => f.PickRandom(ETipoUnidade.d.ToString(),
                ETipoUnidade.m.ToString(), ETipoUnidade.h.ToString(),
                ETipoUnidade.M.ToString(), ETipoUnidade.S.ToString()))
             .RuleFor(t => t.Status, f => f.PickRandom(ETipoStatus.Pendente.ToString(),
                ETipoStatus.Atribuida.ToString(), ETipoStatus.EmAndamento.ToString(),
                ETipoStatus.PendenteAprovacao.ToString(), ETipoStatus.Concluida.ToString()))
             .RuleFor(t => t.CriadorId, f => f.Random.Number(1, 100));

        public Faker<TarefaAlterarDAO> RetornarTarefaAlterarDAO() => new Faker<TarefaAlterarDAO>()
            .RuleFor(t => t.Id, f => f.Random.Number(1, 1000))
            .RuleFor(t => t.Titulo, f => f.Lorem.Word())
            .RuleFor(t => t.PrazoValor, f => f.Random.Number(1, 30))
            .RuleFor(t => t.PrazoUnidade, f => f.PickRandom(ETipoUnidade.d.ToString(),
                ETipoUnidade.m.ToString(), ETipoUnidade.h.ToString(),
                ETipoUnidade.M.ToString(), ETipoUnidade.S.ToString()))
            .RuleFor(t => t.Status, f => f.PickRandom(ETipoStatus.Pendente.ToString(),
                ETipoStatus.Atribuida.ToString(), ETipoStatus.EmAndamento.ToString(),
                ETipoStatus.PendenteAprovacao.ToString(), ETipoStatus.Concluida.ToString()))
            .RuleFor(t => t.DataInicio, f => f.Date.Past().ToString("yyyy-MM-dd"))
            .RuleFor(t => t.DataFim, f => f.Date.Future().ToString("yyyy-MM-dd"))
            .RuleFor(t => t.ResponsavelId, f => f.Random.Number(1, 100));
    }
}
