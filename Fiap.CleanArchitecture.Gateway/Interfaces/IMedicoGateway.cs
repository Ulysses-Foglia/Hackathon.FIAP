using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface IMedicoGateway
    {
        string GerarToken(Medico medico);
        IEnumerable<Medico> BuscarTodos();
        Medico BuscarPorId(int id);
        void Criar(Medico medico);
        Medico Alterar(Medico medico);
        void Excluir(int id);
    }

}
