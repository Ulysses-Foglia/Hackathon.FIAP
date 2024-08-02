using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Presenter
{
    public class MedicoPresenter
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Papel { get; set; }

        public MedicoPresenter(Medico medicoEntity)
        {
            Id = medicoEntity.Id;
            DataCriacao = medicoEntity.DataCriacao;
            Nome = medicoEntity.Nome;
            Email = medicoEntity.Email;
            Papel = medicoEntity.Papel.ToString();
        }

        public static string ToJson(Medico medicoEntity)
        {
            var medicoPresenter = new MedicoPresenter(medicoEntity);

            return JsonConvert.SerializeObject(medicoPresenter);
        }

        public static string ToJson(IEnumerable<Medico> medicosEntity)
        {
            List<MedicoPresenter> medicosPresenter = new List<MedicoPresenter>();

            foreach (var medicoEntity in medicosEntity)
            {
                var medicoPresenter = new MedicoPresenter(medicoEntity);

                medicosPresenter.Add(medicoPresenter);
            }

            return JsonConvert.SerializeObject(medicosPresenter);
        }
    }
}
