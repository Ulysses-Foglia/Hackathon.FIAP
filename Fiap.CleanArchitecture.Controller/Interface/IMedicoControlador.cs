using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IMedicoControlador
    {
        string GerarToken(MedicoDAO medicoDAO);
        string BuscarTodos();
        string BuscarPorId(int id);
        void Criar(MedicoDAO medicoDAO);
        string Alterar(MedicoAlterarDAO medicoAlterarDAO);
        void Excluir(int id);
    }

}
