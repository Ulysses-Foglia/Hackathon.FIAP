using Bogus;
using Dapper;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL.Repositories;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Fiap.CleanArchitecture.Tests.Data
{
    public class TarefaRepository
    {
        private TarefaSQLRepository _tarefaSQLRepository;
        private readonly Faker<Tarefa> _fakerTarefa;
        private Provider _provider;
        private string _connectionString;

        public TarefaRepository()
        {
            _provider = new Provider();

            var configuration = _provider.Configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                throw new("String de conexão inválida");

            _tarefaSQLRepository = new TarefaSQLRepository(configuration);
            _fakerTarefa = RetornarTarefaFaker();

            TesteCriacaoTabela();
        }

        [Fact]
        public void Deve_Inserir_Tarefa_Com_Successo()
        {
            Tarefa tarefa = _fakerTarefa.Generate();

            tarefa.Criador.Id = 1;

            _tarefaSQLRepository.Criar(tarefa);
        }

        private Faker<Tarefa> RetornarTarefaFaker()
        {
            var usuarioFaker = new Faker<Usuario>()
             .RuleFor(u => u.Id, f => f.IndexFaker)
             .RuleFor(u => u.Nome, f => f.Person.FullName)
             .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Nome));

            var prazoFaker = new Faker<Prazo>()
                .RuleFor(p => p.Valor, f => f.Random.Number(1, 30))
                .RuleFor(p => p.Unidade, f => f.PickRandom<ETipoUnidade>());

            var tarefaFaker = new Faker<Tarefa>()
                .RuleFor(t => t.Id, f => f.IndexFaker)
                .RuleFor(t => t.Titulo, f => f.Lorem.Sentence())
                .RuleFor(t => t.Descricao, f => f.Lorem.Paragraph())
                .RuleFor(t => t.Prazo, f => prazoFaker.Generate())
                .RuleFor(t => t.Status, f => f.PickRandom<ETipoStatus>())
                .RuleFor(t => t.DataInicio, f => f.Date.Past())
                .RuleFor(t => t.DataFim, (f, t) => t.Status == ETipoStatus.Concluida ? f.Date.Past() : null)
                .RuleFor(t => t.Criador, f => usuarioFaker.Generate())
                .RuleFor(t => t.Responsavel, f => usuarioFaker.Generate());

            return tarefaFaker;
        }

        private void TesteCriacaoTabela()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string criarTabelaScript = @"

                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TabelaTeste')
                    BEGIN
                        CREATE TABLE TabelaTeste (
                            IDTarefa INT PRIMARY KEY,
                            Titulo NVARCHAR(255),
                            Descricao NVARCHAR(MAX),
                            DataVencimento DATE
                        );
                    END;

                ";

                conn.Execute(criarTabelaScript);
            }
        }
    }
}
