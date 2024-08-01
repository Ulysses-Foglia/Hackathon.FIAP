using Azure.Messaging.ServiceBus;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Fiap.CleanArchitecture.Gateway
{
    public class EmailGateway : IEmailGateway
    {
        private readonly IConfiguration _configuration;

        public EmailGateway(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailMessage(string email, string mensagem)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new { email, mensagem });

                var messageToSend = new ServiceBusMessage(json);

                var client = new ServiceBusClient(_configuration["Azure:ServiceBusConnectionString"]);
                var sender = client.CreateSender(_configuration["Azure:EmailTopicName"]);

                try
                {
                    await sender.SendMessageAsync(messageToSend);

                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }
                catch (Exception ex)
                {
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                    
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
