using Fiap.CleanArchitecture.Entities;
using Fiap.CleanArchitecture.Entity.DAOs.Usuarios;
using Fiap.CleanArchitecture.Entity.Enums;
using Fiap.CleanArchitecture.Entity.Models;
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

        public Usuario() { }

        public Usuario(string email, string senha)
        {
            if (!EmailValido(email))
                throw new Exception(MensagensValidacoes.Usuario_Email);

            if (!SenhaValida(senha))
                throw new Exception(MensagensValidacoes.Usuario_Senha);

            Email = email;
            Senha = Crypto.Encode(senha);
        }

        public Usuario(UsuarioDAO usuarioDAO) 
        {
            ValidarEntity(usuarioDAO);

            if (!NomeValido(usuarioDAO.Nome))
                throw new Exception(MensagensValidacoes.Usuario_Nome);

            if (!EmailValido(usuarioDAO.Email))
                throw new Exception(MensagensValidacoes.Usuario_Email);
            
            if (!SenhaValida(usuarioDAO.Senha))
                throw new Exception(MensagensValidacoes.Usuario_Senha);

            if (!PapelValido(usuarioDAO.Papel, out TipoPapel papel))
                throw new Exception(MensagensValidacoes.Usuario_Papel);
            
            Papel = papel;
            Nome = usuarioDAO.Nome;
            Email = usuarioDAO.Email;            
            Senha = Crypto.Encode(usuarioDAO.Senha);
          
        }

        public Usuario(UsuarioAlterarDAO usuarioAlterarDAO)
        {
            if (!NomeValido(usuarioAlterarDAO.Nome))
                throw new Exception(MensagensValidacoes.Usuario_Nome);

            if (!EmailValido(usuarioAlterarDAO.Email))
                throw new Exception(MensagensValidacoes.Usuario_Email);

            if (!PapelValido(usuarioAlterarDAO.Papel, out TipoPapel papel))
                throw new Exception(MensagensValidacoes.Usuario_Papel);

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

        public void ValidarEntity(UsuarioDAO usuarioDAO)
        {
            AssertionConcern.AssertArgumentTrue(NomeValido(usuarioDAO.Nome), MensagensValidacoes.Usuario_Nome);
            AssertionConcern.AssertArgumentTrue(EmailValido(usuarioDAO.Email), MensagensValidacoes.Usuario_Email);
            AssertionConcern.AssertArgumentTrue(SenhaValida(usuarioDAO.Senha), MensagensValidacoes.Usuario_Senha);
            AssertionConcern.AssertArgumentTrue(PapelValido(usuarioDAO.Papel, out TipoPapel papel), MensagensValidacoes.Usuario_Papel);

        }

        public bool PapelValido(string papel, out TipoPapel tipoPapel) 
            => Enum.TryParse(papel, out tipoPapel);

    

    }
}
