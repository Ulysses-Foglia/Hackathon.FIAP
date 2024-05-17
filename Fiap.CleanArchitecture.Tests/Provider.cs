using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Fiap.CleanArchitecture.Gateway;
using Microsoft.Extensions.DependencyInjection;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.UseCase.Interfaces;
using Fiap.CleanArchitecture.UseCase;

namespace Fiap.CleanArchitecture.Tests
{
    public class Provider
    {
        private readonly ServiceProvider _serviceDescriptors;        
        private IConfiguration _config;

        public Provider()
        {
                     
            var services = new ServiceCollection();

            services.AddScoped<IUsuarioController, UsuarioController>();
            services.AddScoped<IUsuarioGateway, UsuarioGateway>();
            services.AddScoped<ITarefaGateway, TarefaGateway>();
            services.AddScoped<ITarefaUseCase, TarefaUseCase>();
            services.AddScoped<IDatabaseClient>(provider =>
            { return new SQLDatabaseClient(Configuration); });
            services.AddScoped<IUsuarioControlador, UsuarioControlador>();

            _serviceDescriptors = services.BuildServiceProvider();
        }

        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }

        private ServiceProvider GetServices()
        {
            return _serviceDescriptors;
        }


        public T GetRequiredService<T>()
        {
            var provider = GetServices();

            return provider.GetRequiredService<T>();
        }
    }
}
