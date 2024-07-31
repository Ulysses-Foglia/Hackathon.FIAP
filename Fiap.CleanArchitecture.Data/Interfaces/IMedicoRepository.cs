using Fiap.CleanArchitecture.Entity.Entities;


namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IMedicoRepository
    {
        string GerarToken(Medico medico);
        IEnumerable<Medico> BuscarTodosMedicos();
        Medico BuscarMedicoPorId(int id);
        void CriarMedico(Medico medico);
        Medico AlterarMedico(Medico medico);
        void ExcluirMedico(int id);
    }
}
