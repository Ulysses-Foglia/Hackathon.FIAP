using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Fiap.CleanArchitecture.Util
{
    public static class Ferramentas
    {
        public static T GenericEnumTryParse<T>(string statusString, out T status) where T : struct, Enum
        {
            Enum.TryParse(statusString, out status);

            return status;
        }

        public static DateTime GenericDataTryParse<T>(string DataInicio, DateTime dataInicio) where T : struct, Enum
        {            
            DateTime.TryParse(DataInicio, out dataInicio);

            return dataInicio;
        }

        public static string? GetGenericEnumDescription<TEnum>(TEnum value) where TEnum : Enum
        {
            var attribute = value?.GetType()?.GetField(value.ToString())?.GetCustomAttribute<DescriptionAttribute>();

            return attribute != null ? attribute.Description : value?.ToString();
        }

        public static string FormatarString(string valor)
        {
            return Regex.Replace(valor, "[^a-zA-Z0-9]", "");
        }
    }
}
