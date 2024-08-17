
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace KHN.Cons.Infrastructure
{
    public static class Utility
    {
        public static string ConvertObjectToJson(object theObject, bool writeIndented = true)
        {
            var options = new  JsonSerializerOptions{ 
                WriteIndented = writeIndented,
            };

            var result = JsonSerializer.Serialize(theObject, options);

            return result;
        }

        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }


        public static string GetSha256(string text)
        {
            var inputBytes = Encoding.UTF8.GetBytes(text);

            var sha = SHA256.Create();

            var outPutBytes = sha.ComputeHash(inputBytes);

            sha.Dispose();

            var result = Convert.ToBase64String(outPutBytes);   

            return result;
        }
    }
}
