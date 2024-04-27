using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Presenter
{
    public class UsuarioPresenter
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public static string ToJson(Usuario usuarioEntity)
        {
            var usuarioPresenter = new UsuarioPresenter()
            {
                Email = usuarioEntity.Email,
                Senha = usuarioEntity.Senha
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
                    Email = usuarioEntity.Email,
                    Senha = usuarioEntity.Senha
                };

                usuariosPresenter.Add(usuarioPresenter);
            }            

            return JsonConvert.SerializeObject(usuariosPresenter);
        }
    }
}
