namespace Fiap.CleanArchitecture.Data.Interfaces
{
    public interface IDatabaseClient : IUsuarioRepository, ITarefaRepository, IAgendaRepository, IMedicoRepository
    {
    }
}
