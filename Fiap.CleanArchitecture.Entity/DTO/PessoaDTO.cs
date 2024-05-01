using Fiap.CleanArchitecture.Entity.Entities;

namespace Fiap.CleanArchitecture.Entity.DTO
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public int UsuarioId { get; set; }
        public DateTime UsuarioDataCriacao { get; set; }
        public string UsuarioEmail { get; set; }

        public static Pessoa ToEntity(PessoaDTO pessoaDTO)
        {
            return new Pessoa(pessoaDTO.Nome)
            {
                Id = pessoaDTO.Id,
                DataCriacao = pessoaDTO.DataCriacao,
                Usuario = pessoaDTO.UsuarioId == 0 ? null : new Usuario()
                {
                    Id = pessoaDTO.UsuarioId,
                    DataCriacao = pessoaDTO.UsuarioDataCriacao,
                    Email = pessoaDTO.UsuarioEmail
                }
            };
        }

        public static List<Pessoa> ToEntity(IEnumerable<PessoaDTO> pessoasDTO)
        {
            List<Pessoa> pessoas = [];

            foreach (var pessoaDTO in pessoasDTO)
                pessoas.Add(ToEntity(pessoaDTO));

            return pessoas;
        }
    }
}
