namespace Fiap.CleanArchitecture.Gateway.Interfaces
{
    public interface IEmailGateway
    {
        Task SendMailMessage(string message);
    }
}
