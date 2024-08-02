using Fiap.CleanArchitecture.Entity.DAOs.Email;

namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface IEmailGateway
    {
        Task SendMailMessage(EmailDAO emailDAO);
    }
}
