using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Entity.DAOs.Email;
using Fiap.CleanArchitecture.Gateway.Interfaces;

namespace Fiap.CleanArchitecture.Controller
{
    public class EmailControlador : IEmailControlador
    {
        private readonly IEmailGateway _emailGateway;

        public EmailControlador(IEmailGateway emailGateway)
        {
            _emailGateway = emailGateway;
        }

        public async Task SendMail(EmailDAO emailDAO) 
            => await _emailGateway.SendMailMessage(emailDAO);
    }
}
