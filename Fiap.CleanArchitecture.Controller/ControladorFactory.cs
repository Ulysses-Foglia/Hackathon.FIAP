using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;

namespace Fiap.CleanArchitecture.Controller
{
    public class ControladorFactory<T> : IControladorFactory<T> where T : class
    {
        public T CriarControlador(IDatabaseClient databaseClient)
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(IDatabaseClient) });

            if (constructor == null)
                throw new InvalidOperationException($"O tipo {typeof(T).Name} não tem um construtor que aceita um IDatabaseClient.");

            return (T)constructor.Invoke(new object[] { databaseClient });
        }
    }
}
