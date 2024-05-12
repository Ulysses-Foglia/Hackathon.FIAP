using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.CleanArchitecture.Util
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VersaoApi : Attribute
    {
        public string VersaoDaApi { get; set; }

        public VersaoApi()
        {

        }

        public VersaoApi(string versaoDaApi)
        {
            VersaoDaApi = versaoDaApi;
        }
    }
}
