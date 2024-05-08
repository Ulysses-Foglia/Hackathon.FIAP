using Fiap.CleanArchitecture.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Controller
{
    public interface IControladorFactory<T>
    {
        T CriarControlador(IDatabaseClient databaseClient);
    }
}
