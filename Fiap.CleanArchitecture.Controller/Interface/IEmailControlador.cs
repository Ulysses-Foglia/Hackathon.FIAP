namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IEmailControlador
    {
        Task SendMail(string message);
    }
}
