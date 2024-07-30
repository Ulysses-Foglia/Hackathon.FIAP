using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.ServiceBus
{
    public class SendMail
    {
        [FunctionName("SendMail")]
        public async Task Run([ServiceBusTrigger("send-mail", Connection = "ServiceBusConnectionString")]
        string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            await Task.Delay(1);
        }
    }
}
