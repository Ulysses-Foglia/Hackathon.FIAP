using Bogus;
using Fiap.CleanArchitecture.Entity.DAOs.Tarefa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using FluentAssertions;

namespace Fiap.CleanArchitecture.Tests.UseCases
{
    public class TarefaUseCaseTests
    {
        private Provider _provider;
        private ITarefaUseCase _tarefaUseCase;
        private Faker<TarefaDAO> _fakerTarefaDao;

        public TarefaUseCaseTests()
        {
            _provider = new Provider();

            _tarefaUseCase = _provider.GetRequiredService<ITarefaUseCase>();

            _fakerTarefaDao = RetornarFakerTarefaDao();
        }

        [Fact]
        public void Deve_Alterar_Situacao_Com_Successo()
        {
            var resultaEsperadoTarefa = new Tarefa
            {
                Id = 1,
                Titulo = "Criar aplicação",
                Descricao = "Criar aplicação",
                Prazo = new Prazo { Valor = 4, Unidade = ETipoUnidade.d },
                Status = ETipoStatus.Concluida,
                DataCriacao = new DateTime(2024, 5, 12, 11, 11, 27, 833),
                DataInicio = new DateTime(2024, 4, 26, 10, 0, 0),
                DataFim = new DateTime(2024, 4, 30, 10, 0, 0),
                Criador = new Usuario
                {
                    Id = 1,
                    Nome = "UsuTeste1",
                    Papel = TipoPapel.Admin,
                    Email = "usuTeste1@email.com.br"
                },
                Responsavel = new Usuario
                {
                    Id = 2,
                    Nome = "teste123",
                    Papel = TipoPapel.Admin,
                    Email = "usuTeste1@email.com.br"
                }
            };

            var retornoTarefaUseCase = _tarefaUseCase.AltereSituacao(1, ETipoStatus.Concluida);

            try
            {
                resultaEsperadoTarefa.Should().BeEquivalentTo(retornoTarefaUseCase);

                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Deve_Atribuir_Responsavel_Com_Successo()
        {
            var resultaEsperadoTarefa = new Tarefa
            {
                Id = 1,
                Titulo = "Criar aplicação",
                Descricao = "Criar aplicação",
                Prazo = new Prazo { Valor = 4, Unidade = ETipoUnidade.d },
                Status = ETipoStatus.EmAndamento,
                DataCriacao = new DateTime(2024, 5, 12, 11, 11, 27, 833),
                DataInicio = new DateTime(2024, 4, 26, 10, 0, 0),
                DataFim = new DateTime(2024, 4, 30, 10, 0, 0),
                Criador = new Usuario
                {
                    Id = 1,
                    Nome = "UsuTeste1",
                    Papel = TipoPapel.Admin,
                    Email = "usuTeste1@email.com.br",
                    DataCriacao = new DateTime(0001, 01, 01)
                },
                Responsavel = new Usuario
                {
                    Id = 2,
                    Nome = "teste123",
                    Papel = TipoPapel.Admin,
                    Email = "usuTeste1@email.com.br",
                    DataCriacao = new DateTime(0001, 01, 01)
                }
            };

            var retornoTarefaUseCase = _tarefaUseCase.AtribuaUmResponsavel(1, ETipoStatus.EmAndamento, 2);

            try
            {
                resultaEsperadoTarefa.Should().BeEquivalentTo(retornoTarefaUseCase);

                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Deve_Registrar_Tarefa_Com_Sucesso()
        {
            var taredaFakerDao = _fakerTarefaDao.Generate();
            taredaFakerDao.CriadorId = 1;

            var retornoTarefaUseCase = _tarefaUseCase.RegistreTarefa(taredaFakerDao);
            retornoTarefaUseCase.Should().BeEquivalentTo(retornoTarefaUseCase);

            Assert.Equal(taredaFakerDao, retornoTarefaUseCase);
        }

        public Faker<TarefaDAO> RetornarFakerTarefaDao() => new Faker<TarefaDAO>()
            .RuleFor(t => t.Titulo, f => f.Lorem.Sentence())
            .RuleFor(t => t.Descricao, f => f.Lorem.Paragraph())
            .RuleFor(t => t.PrazoValor, f => f.Random.Number(1, 30))
            .RuleFor(t => t.PrazoUnidade, f => f.PickRandom("dias", "semanas", "meses"))
            .RuleFor(t => t.Status, f => f.PickRandom("Pendente", "Atribuida", "EmAndamento", "PendenteAprovacao", "Concluida"))
            .RuleFor(t => t.CriadorId, f => f.Random.Number(1, 100));
    }
}
