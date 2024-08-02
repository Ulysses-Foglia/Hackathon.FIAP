using Fiap.CleanArchitecture.Entity.DAOs.Email;
using Fiap.CleanArchitecture.Entity.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.ServiceBus
{
    public class SendMail
    {
        private readonly IConfiguration _configuration;

        public SendMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("SendMail")]
        public async Task Run([ServiceBusTrigger("send-mail", Connection = "AzureWebJobsStorage")]
        string myQueueItem, ILogger log)
        {
            var mailDAO = JsonConvert.DeserializeObject<EmailDAO>(myQueueItem);

            var mensagemEmail = new MensagemEmail(_configuration)
            {
                To = new MailAddress(mailDAO.MedicoEmail),
                Subject = "Health&Med - Nova consulta agendada!"
            };

            mensagemEmail.Message = mensagemEmail.HTML
                .Replace("@MedicoNome", mailDAO.MedicoNome)
                .Replace("@PacienteNome ", mailDAO.PacienteNome)
                .Replace("@DataHora_Data", mailDAO.DataHora.ToString("dd/MM/yyyy"))
                .Replace("@DataHora_Hora", mailDAO.DataHora.ToString("HH:mm"));

            try
            {
                var mailMessage = new MailMessage()
                {
                    Subject = mensagemEmail.Subject,
                    From = mensagemEmail.From,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Body = mensagemEmail.Message,
                };

                mailMessage.To.Add(mensagemEmail.To);

                var mailClient = new SmtpClient(mensagemEmail.Host, mensagemEmail.Port)
                {
                    EnableSsl = mensagemEmail.SSL,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mensagemEmail.From.Address, mensagemEmail.Password)
                };

                await mailClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
