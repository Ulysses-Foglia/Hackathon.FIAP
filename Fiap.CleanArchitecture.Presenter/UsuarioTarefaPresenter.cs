using Fiap.CleanArchitecture.Entity.Entities;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Presenter
{
    public class UsuarioTarefaPresenter
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Papel { get; set; }

        public UsuarioTarefaPresenter(Usuario usuarioEntity)
        {
            if (usuarioEntity != null)
            {
                Nome = usuarioEntity.Nome;
                Email = usuarioEntity.Email;
                Papel = usuarioEntity.Papel.ToString();
            }
        }

        public static string ToJson(Usuario usuarioEntity)
        {
            var usuarioTarefaPresenter = new UsuarioTarefaPresenter(usuarioEntity);

            return JsonConvert.SerializeObject(usuarioTarefaPresenter);
        }

        public static string ToJson(IEnumerable<Usuario> usuariosEntity)
        {
            List<UsuarioTarefaPresenter> usuariosTarefaPresenter = [];

            foreach (var usuarioEntity in usuariosEntity)
            {
                var usuarioTarefaPresenter = new UsuarioTarefaPresenter(usuarioEntity);

                usuariosTarefaPresenter.Add(usuarioTarefaPresenter);
            }

            return JsonConvert.SerializeObject(usuariosTarefaPresenter);
        }
    }
}
