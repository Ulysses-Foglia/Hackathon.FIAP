using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Presenter
{
    public class TarefaPresenter
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Titulo { get; set; }
        public PrazoPresenter Prazo { get; set; }
        public string Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public UsuarioTarefaPresenter Criador { get; set; }
        public UsuarioTarefaPresenter Responsavel { get; set; }

        public TarefaPresenter(Tarefa tarefaEntity)
        {
            Id = tarefaEntity.Id;
            DataCriacao = tarefaEntity.DataCriacao;
            Titulo = tarefaEntity.Titulo;
            Prazo = new PrazoPresenter(tarefaEntity.Prazo);
            Status = tarefaEntity.Status.ToString();
            DataInicio = tarefaEntity.DataInicio;
            DataFim = tarefaEntity.DataFim;
            Criador = new UsuarioTarefaPresenter(tarefaEntity.Criador);
            Responsavel = new UsuarioTarefaPresenter(tarefaEntity.Responsavel);
        }

        public static string ToJson(Tarefa tarefaEntity)
        {
            var tarefaPresenter = new TarefaPresenter(tarefaEntity); ;

            return JsonConvert.SerializeObject(tarefaPresenter);
        }

        public static string ToJson(IEnumerable<Tarefa> tarefasEntity)
        {
            List<TarefaPresenter> tarefasPresenter = [];

            foreach (var tarefaEntity in tarefasEntity)
            {
                var tarefaPresenter = new TarefaPresenter(tarefaEntity);

                tarefasPresenter.Add(tarefaPresenter);
            }

            return JsonConvert.SerializeObject(tarefasPresenter);
        }
    }
}
