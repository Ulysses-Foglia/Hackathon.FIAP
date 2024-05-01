using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Gateway
{
    public class PessoaGateway : IPessoaGateway
    {
        private readonly IDatabaseClient _database;

        public PessoaGateway(IDatabaseClient database)
        {
            _database = database;
        }

        public IEnumerable<Pessoa> BuscarTodos() => _database.BuscarTodasPessoas();
        public Pessoa BuscarPorId(int id) => _database.BuscarPessoaPorId(id);
        public void Criar(Pessoa pessoa) => _database.CriarPessoa(pessoa);
        public Pessoa Alterar(Pessoa pessoa) => _database.AlterarPessoa(pessoa);
        public void Excluir(int id) => _database.ExcluirPessoa(id);
    }
}
