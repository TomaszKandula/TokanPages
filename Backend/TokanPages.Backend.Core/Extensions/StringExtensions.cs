using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace TokanPages.Backend.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64Encode(this string APlainText)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(APlainText));

        public static string ToBase64Decode(this string ABase64EncodedData)
            => Encoding.UTF8.GetString(Convert.FromBase64String(ABase64EncodedData));
		
        public static bool IsBase64String(this string ABase64)
        {
            var LBuffer = new Span<byte>(new byte[ABase64.Length]);
            var LWidth = ABase64.Length / 4 * 4 + (ABase64.Length % 4 == 0 ? 0 : 4);
            return Convert.TryFromBase64String(ABase64.PadRight(LWidth, '='), LBuffer, out _);
        }

        public static bool IsValidConnectionString(this string AConnectionString)
        {
            try
            {
                // ReSharper disable once UnusedVariable
                var LConnectionString = new SqlConnectionStringBuilder(AConnectionString);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}