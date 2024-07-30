using Azure.Messaging.ServiceBus;
using Fiap.CleanArchitecture.Gateway.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Fiap.CleanArchitecture.Gateway
{
    public class EmailGateway : IEmailGateway
    {
        private readonly IConfiguration _configuration;

        public EmailGateway(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailMessage(string message)
        {
            try
            {
                var messageToSend = new ServiceBusMessage(message);

                var client = new ServiceBusClient(_configuration["Azure:ServiceBusConnectionString"]);
                var sender = client.CreateSender(_configuration["Azure:EmailTopicName"]);

                try
                {
                    await sender.SendMessageAsync(messageToSend);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
