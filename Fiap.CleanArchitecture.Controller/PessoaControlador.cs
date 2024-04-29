using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Entity.Entities;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Controller
{
    public class PessoaControlador
    {
        //private readonly ILoginGateway _loginGateway;
        //private readonly IDatabaseClient _databaseClient;

        public PessoaControlador(IDatabaseClient databaseClient)
        {
            //_databaseClient = databaseClient;
            //_loginGateway = new LoginGateway(_databaseClient);
        }

        //public string GerarToken(Usuario usuario)
        //{
        //    var token = _loginGateway.GerarToken(usuario);

        //    return token;
        //}
    }
}
