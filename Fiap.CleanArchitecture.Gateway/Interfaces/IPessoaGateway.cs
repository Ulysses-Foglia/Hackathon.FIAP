using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface IPessoaGateway
    {
        IEnumerable<Pessoa> BuscarTodos();
        Pessoa BuscarPorId(int id);
        void Criar(Pessoa pessoa);
        Pessoa Alterar(Pessoa pessoa);
        void Excluir(int id);
    }
}
