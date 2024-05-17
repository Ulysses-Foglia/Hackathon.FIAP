namespace Fiap.CleanArchitecture.Util
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VersaoApi : Attribute
    {
        public string VersaoDaApi { get; set; }

        public VersaoApi(string versaoDaApi)
        {
            VersaoDaApi = versaoDaApi;
        }
    }
}
