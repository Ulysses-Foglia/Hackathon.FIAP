namespace Fiap.CleanArchitecture.Entity.Entities
{
    public class Usuario : EntityBase
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        protected Usuario() { }

        public Usuario(string email, string senha) 
        {
            Email = email;
            Senha = senha;
        }
    }
}
