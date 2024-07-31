using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.UseCase.Interfaces
{
    public interface IMedicoUseCase
    {
        void CadastreNovoMedico(MedicoDAO medico);
        Medico AltereMedico(MedicoAlterarDAO medico);
        string AutentiqueMedico(MedicoDAO medico);
        void ExcluaMedico(int idMedico);
    }
}
