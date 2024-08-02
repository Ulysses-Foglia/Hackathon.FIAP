using Fiap.CleanArchitecture.Entity.DAOs.Email;

namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IEmailControlador
    {
        Task SendMail(EmailDAO emailDAO);
    }
}
