using Fiap.CleanArchitecture.Controller.Interface;
using Fiap.CleanArchitecture.Data.Interfaces;
using Newtonsoft.Json.Linq;

namespace Fiap.CleanArchitecture.Controller
{
    public class ControladorFactory<T> : IControladorFactory<T> where T : class
    {
        public T CriarControlador(IDatabaseClient databaseClient)
        {
            var constructors = typeof(T).GetConstructors();

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();

                if (parameters.Any(p => p.ParameterType == typeof(IDatabaseClient)))
                    return (T)constructor.Invoke(new object[] { databaseClient });
            }

            throw new InvalidOperationException($"O tipo {typeof(T).Name} não tem um construtor que aceita um IDatabaseClient.");

        }
    }
}
