using Fiap.CleanArchitecture.Api.Controllers;
using Fiap.CleanArchitecture.Api.Controllers.Interfaces;
using Fiap.CleanArchitecture.Controller;
using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.DatabaseClients.SQL;
using Fiap.CleanArchitecture.Data.Interfaces;
using Fiap.CleanArchitecture.Gateway;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.CleanArchitecture.Tests
{
    public class Provider
    {
        private readonly ServiceProvider _serviceDescriptors;
        private IConfiguration _config;

        public Provider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IUsuarioControlador, UsuarioControlador>();
            services.AddScoped<IUsuarioController, UsuarioController>();
            services.AddScoped<IUsuarioGateway, UsuarioGateway>();

            services.AddScoped<IAgendaController, AgendaController>();
            services.AddScoped<IAgendaControlador, AgendaControlador>();
            services.AddScoped<IAgendaGateway, AgendaGateway>();

            services.AddScoped<IMedicoControlador, MedicoControlador>();
            services.AddScoped<IMedicoController, MedicoController>();
            services.AddScoped<IMedicoGateway, MedicoGateway>();

         
            services.AddScoped<IDatabaseClient>(provider => new SQLDatabaseClient(Configuration));

            _serviceDescriptors = services.BuildServiceProvider();
        }

        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.json", optional: false);

                    _config = builder.Build();
                }

                return _config;
            }
        }

        private ServiceProvider GetServices() => _serviceDescriptors;

        public T GetRequiredService<T>()
        {
            var provider = GetServices();

            return provider.GetRequiredService<T>();
        }
    }
}
