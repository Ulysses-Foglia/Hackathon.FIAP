using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Presenter
{
    public class UsuarioPresenter
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Email { get; set; }

        public static string ToJson(Usuario usuarioEntity)
        {
            var usuarioPresenter = new UsuarioPresenter()
            {
                Id = usuarioEntity.Id,
                DataCriacao = usuarioEntity.DataCriacao,
                Email = usuarioEntity.Email
            };

            return JsonConvert.SerializeObject(usuarioPresenter);
        }

        public static string ToJson(IEnumerable<Usuario> usuariosEntity)
        {
            List<UsuarioPresenter> usuariosPresenter = [];

            foreach (var usuarioEntity in usuariosEntity)
            {
                var usuarioPresenter = new UsuarioPresenter()
                {
                    Id = usuarioEntity.Id,
                    DataCriacao = usuarioEntity.DataCriacao,
                    Email = usuarioEntity.Email
                };

                usuariosPresenter.Add(usuarioPresenter);
            }            

            return JsonConvert.SerializeObject(usuariosPresenter);
        }
    }
}
