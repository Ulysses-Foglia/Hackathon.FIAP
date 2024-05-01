using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.DAOs.Pessoa;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Presenter;

namespace Fiap.CleanArchitecture.Controller
{
    public class PessoaControlador
    {
        private readonly IPessoaGateway _pessoaGateway;
        private readonly IDatabaseClient _databaseClient;

        public PessoaControlador(IDatabaseClient databaseClient)
        {
            _databaseClient = databaseClient;
            _pessoaGateway = new PessoaGateway(_databaseClient);
        }

        public string BuscarTodos()
        {
            var pessoas = _pessoaGateway.BuscarTodos();

            return PessoaPresenter.ToJson(pessoas);
        }

        public string BuscarPorId(int id)
        {
            var pessoa = _pessoaGateway.BuscarPorId(id);

            return PessoaPresenter.ToJson(pessoa);
        }

        public void Criar(PessoaDAO pessoaDAO)
        {
            var pessoa = new Pessoa(pessoaDAO.Nome);

            _pessoaGateway.Criar(pessoa);
        }

        public string Alterar(PessoaAlterarDAO pessoaAlterarDAO)
        {
            var pessoa = new Pessoa(pessoaAlterarDAO);

            var novaPessoa = _pessoaGateway.Alterar(pessoa);

            return PessoaPresenter.ToJson(novaPessoa);
        }

        public void Excluir(int id)
        {
            _pessoaGateway.Excluir(id);
        }
    }
}
