using Fiap.CleanArchitecture.Entity.Models;

namespace Fiap.CleanArchitecture.Presenter
{
    public class PrazoPresenter
    {
        public int Valor { get; set; }
        public string Unidade { get; set; }

        public PrazoPresenter(Prazo prazo) 
        {
            Valor = prazo.Valor;
            Unidade = prazo.Unidade.ToString();
        }
    }
}
