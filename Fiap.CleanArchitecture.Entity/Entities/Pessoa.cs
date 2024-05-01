using Fiap.CleanArchitecture.Entity.DAOs.Pessoa;

namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Pessoa : EntityBase
    {
        public string Nome { get; set; }
        public Usuario Usuario { get; set; }
        public IEnumerable<Tarefa> Tarefas { get; set; }

        private readonly string MensagemNomeInvalido 
            = "O nome deve possuir no máximo 100 caracteres!";

        public Pessoa() { }

        public Pessoa(string nome)
        {
            if (!NomeValido(nome))
                throw new Exception(MensagemNomeInvalido);

            Nome = nome;
            Usuario = null;
            Tarefas = [];
        }

        public Pessoa(PessoaAlterarDAO pessoaAlterarDAO)
        {
            if (!NomeValido(pessoaAlterarDAO.Nome))
                throw new Exception(MensagemNomeInvalido);

            Id = pessoaAlterarDAO.Id;
            Nome = pessoaAlterarDAO.Nome;
            Usuario = Usuario.AtribuirUsuario(pessoaAlterarDAO.UsuarioId);
        }

        private bool NomeValido(string nome) => nome.Length <= 100;
    }
}
