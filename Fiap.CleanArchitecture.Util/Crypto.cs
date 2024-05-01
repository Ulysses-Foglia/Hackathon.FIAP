using System.Text;

namespace Fiap.CleanArchitecture.Util
{
    public static class Crypto
    {
        public static string Encode(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            
            return Convert.ToBase64String(textBytes);
        }

        public static string Decode(string text)
        {
            byte[] textBase64 = Convert.FromBase64String(text);
            
            return Encoding.UTF8.GetString(textBase64);
        }
    }
}
