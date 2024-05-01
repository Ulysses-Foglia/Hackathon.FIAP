using Microsoft.Extensions.Configuration;

namespace Fiap.CleanArchitecture.Data
{
    public class Repository
    {
        public string ConnectionString { get; private set; }
        public int Timeout { get; private set; }

        public Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            if (int.TryParse(configuration.GetSection("Configuration:Timeout").Value, out var secondsToInt))
                Timeout = secondsToInt;
            else
                throw new Exception("Erro ao converter a configuração de timeout da aplicação!");
        }
    }
}
