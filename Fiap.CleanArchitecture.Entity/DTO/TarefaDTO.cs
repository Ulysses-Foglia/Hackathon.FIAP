using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Entity.DTO
{
    public class TarefaDTO
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int PrazoValor { get; set; }
        public string PrazoUnidade { get; set; }
        public string Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int CriadorId { get; set; }
        public string CriadorNome { get; set; }
        public string CriadorEmail { get; set; }
        public int? ResponsavelId { get; set; }
        public string ResponsavelNome { get; set; }
        public string ResponsavelEmail { get; set; }

        public static Tarefa ToEntity(TarefaDTO tarefaDTO)
        {
            return new Tarefa() 
            {
                Id = tarefaDTO.Id,
                DataCriacao = tarefaDTO.DataCriacao,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO?.Descricao,
                Prazo = new Prazo()
                {
                    Valor = tarefaDTO.PrazoValor,
                    Unidade = Enum.Parse<ETipoUnidade>(tarefaDTO.PrazoUnidade)
                },
                Status = Enum.Parse<ETipoStatus>(tarefaDTO.Status),
                DataInicio = tarefaDTO.DataInicio,
                DataFim = tarefaDTO.DataFim,
                Criador = new Usuario()
                {
                    Id = tarefaDTO.CriadorId,
                    Nome = tarefaDTO.CriadorNome,
                    Email = tarefaDTO.CriadorEmail
                },
                Responsavel = tarefaDTO.ResponsavelNome != null ? 
                new Usuario()
                {
                    Id = tarefaDTO.ResponsavelId.GetValueOrDefault(),
                    Nome = tarefaDTO.ResponsavelNome,
                    Email = tarefaDTO.ResponsavelEmail
                } : null
            };
        }

        public static List<Tarefa> ToEntity(IEnumerable<TarefaDTO> tarefasDTO)
        {
            List<Tarefa> tarefas = [];

            foreach (var tarefaDTO in tarefasDTO)                
                    tarefas.Add(ToEntity(tarefaDTO));              
                 
            return tarefas;
        }
    }
}
