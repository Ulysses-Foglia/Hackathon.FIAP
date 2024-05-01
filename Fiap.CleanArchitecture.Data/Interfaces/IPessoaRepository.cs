using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IPessoaRepository
    {
        IEnumerable<Pessoa> BuscarTodasPessoas();
        Pessoa BuscarPessoaPorId(int id);
        void CriarPessoa(Pessoa usuario);
        Pessoa AlterarPessoa(Pessoa usuario);
        void ExcluirPessoa(int id);
    }
}
