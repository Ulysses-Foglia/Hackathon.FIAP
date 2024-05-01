using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Util;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Usuario : EntityBase
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        private readonly string MensagemEmailInvalido
            = "Email no formato incorreto!";

        private readonly string MensagemSenhaInvalida
            = "A senha deve possuir entre 6 e 20 caracteres!";

        public Usuario() { }

        public Usuario(string email, string senha) 
        {
            if (!EmailValido(email))
                throw new Exception(MensagemEmailInvalido);
            
            if (!SenhaValida(senha))
                throw new Exception(MensagemSenhaInvalida);

            Email = email;            
            Senha = Crypto.Encode(senha);
        }

        public Usuario(UsuarioAlterarDAO usuarioAlterarDAO)
        {
            if (!EmailValido(usuarioAlterarDAO.Email))
                throw new Exception(MensagemEmailInvalido);

            Id = usuarioAlterarDAO.Id;
            Email = usuarioAlterarDAO.Email;
        }

        private bool EmailValido(string email)
        {
            return email.Contains('@') 
                && email.Contains(".com") 
                && email.Length <= 256;
        }

        private bool SenhaValida(string senha)
        {
            return senha.Length >= 6
                && senha.Length <= 20;
        }

        public static Usuario AtribuirUsuario(int id)
        {
            return new Usuario() { Id = id };
        }
    }
}
