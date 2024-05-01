using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Entity.DTO
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Titulo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int CriadorId { get; set; }
        public DateTime CriadorDataCriacao { get; set; }
        public string CriadorNome { get; set; }

        public static Tarefa ToEntity(TarefaDTO tarefaDTO)
        {
            return new Tarefa() 
            {
                Id = tarefaDTO.Id,
                DataCriacao = tarefaDTO.DataCriacao,
                Titulo = tarefaDTO.Titulo,
                DataInicio = tarefaDTO.DataInicio,
                DataFim = tarefaDTO.DataFim,
                Criador = new Pessoa(tarefaDTO.CriadorNome)
                {
                    Id = tarefaDTO.CriadorId,
                    DataCriacao = tarefaDTO.CriadorDataCriacao
                }
            };
        }

        public static List<Tarefa> ToEntity(IEnumerable<TarefaDTO> tarefasDTO)
        {
            List<Tarefa> pessoas = [];

            foreach (var tarefaDTO in tarefasDTO)
                pessoas.Add(ToEntity(tarefaDTO));

            return pessoas;
        }
    }
}
