using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Presenter
{
    public class PessoaPresenter
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public UsuarioPresenter Usuario { get; set; }
        //public IEnumerable<Tarefa> Tarefas { get; set; }

        public PessoaPresenter(Pessoa pessoaEntity)
        {
            Id = pessoaEntity.Id;
            DataCriacao = pessoaEntity.DataCriacao;
            Nome = pessoaEntity.Nome;

            if (pessoaEntity.Usuario != null)
                Usuario = new UsuarioPresenter(pessoaEntity.Usuario);

            //Tarefas
        }

        public static string ToJson(Pessoa pessoaEntity)
        {
            var pessoaPresenter = new PessoaPresenter(pessoaEntity); ;

            return JsonConvert.SerializeObject(pessoaPresenter);
        }

        public static string ToJson(IEnumerable<Pessoa> pessoasEntity)
        {
            List<PessoaPresenter> pessoasPresenter = [];

            foreach (var pessoaEntity in pessoasEntity)
            {
                var pessoaPresenter = new PessoaPresenter(pessoaEntity);

                pessoasPresenter.Add(pessoaPresenter);
            }

            return JsonConvert.SerializeObject(pessoasPresenter);
        }
    }
}
