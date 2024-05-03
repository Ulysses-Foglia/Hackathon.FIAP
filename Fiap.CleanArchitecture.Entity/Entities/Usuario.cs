using Fiap.CleanArchitecture.Entity.DAOs.Usuario;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Util;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoPapel Papel { get; set; }

        public IEnumerable<Tarefa> Tarefas { get; set; }

        private readonly string MensagemNomeInvalido
            = "Nome deve possuir no máximo 100 caracteres!";

        private readonly string MensagemEmailInvalido
            = "Email no formato incorreto!";

        private readonly string MensagemSenhaInvalida
            = "A senha deve possuir entre 6 e 20 caracteres!";

        private readonly string MensagemPapelInvalido
            = "Papel incorreto! Em caso de dúvida, utilize o valor 'Comum'.";

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

        public Usuario(UsuarioDAO usuarioDAO) 
        {
            if (!NomeValido(usuarioDAO.Nome))
                throw new Exception(MensagemNomeInvalido);

            if (!EmailValido(usuarioDAO.Email))
                throw new Exception(MensagemEmailInvalido);
            
            if (!SenhaValida(usuarioDAO.Senha))
                throw new Exception(MensagemSenhaInvalida);

            if (!PapelValido(usuarioDAO.Papel, out TipoPapel papel))
                throw new Exception(MensagemPapelInvalido);

            Nome = usuarioDAO.Nome;
            Email = usuarioDAO.Email;            
            Senha = Crypto.Encode(usuarioDAO.Senha);
            Papel = papel;
        }

        public Usuario(UsuarioAlterarDAO usuarioAlterarDAO)
        {
            if (!NomeValido(usuarioAlterarDAO.Nome))
                throw new Exception(MensagemNomeInvalido);

            if (!EmailValido(usuarioAlterarDAO.Email))
                throw new Exception(MensagemEmailInvalido);

            if (!PapelValido(usuarioAlterarDAO.Papel, out TipoPapel papel))
                throw new Exception(MensagemPapelInvalido);

            Id = usuarioAlterarDAO.Id;
            Nome = usuarioAlterarDAO.Nome;
            Email = usuarioAlterarDAO.Email;
            Papel = papel;
        }

        private bool NomeValido(string nome) => nome.Length <= 100;

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

        public bool PapelValido(string papel, out TipoPapel tipoPapel) 
            => Enum.TryParse(papel, out tipoPapel);
    }
}
