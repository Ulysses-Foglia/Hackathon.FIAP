using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Fiap.CleanArchitecture.Entity.Models
{
    public class MensagemEmail
    {
        public string Host { get; private set; }
        public string Password { get; private set; }
        public int Port { get; private set; }
        public bool SSL { get; private set; }
        public MailAddress From { get; private set; }
        public MailAddress To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string HTML => "<!DOCTYPE html> <html> <head> <meta charset='utf-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <title>EmailGrupo11</title> <meta name='viewport' content='width=device-width, initial-scale=1'/> </head> <body> <div class=\"container\" style=\"background-color:#EE145B;color:white;padding:25px\"> <div class=\"form-group\"> <div class=\"row\"> <p>Olá, Dr. <b>@MedicoNome</b>!</p> </br> <p>Você tem uma nova consulta marcada!</p> </br> <p>Paciente: <b>@PacienteNome</b>.</p> <p>Data e horário: <b>@DataHora_Data</b> às <b>@DataHora_Hora</b>.</p> </div> </div> </div> </body> </html>";

        public MensagemEmail(IConfiguration configuration) 
        {
            Host = configuration["Mail:Host"];
            Password = configuration["Mail:Password"];
            Port = int.Parse(configuration["Mail:Port"]);
            SSL = bool.Parse(configuration["Mail:SSL"]);
            From = new MailAddress(configuration["Mail:From"]);
        }
    }
}
