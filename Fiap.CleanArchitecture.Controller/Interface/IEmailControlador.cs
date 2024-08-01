namespace Fiap.CleanArchitecture.Controller.Interface
{
    public interface IEmailControlador
    {
        Task SendMail(string email, string mensagem);
    }
}
