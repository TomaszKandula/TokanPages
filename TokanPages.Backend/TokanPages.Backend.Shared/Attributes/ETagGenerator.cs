namespace TokanPages.Backend.Shared.Attributes
{
    using System;
    using System.Text;
    using System.Security.Cryptography;

    public static class ETagGenerator
    {
        public static string GetETag(string key, byte[] contentBytes)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var combinedBytes = Combine(keyBytes, contentBytes);
            return GenerateETag(combinedBytes);
        }

        private static string GenerateETag(byte[] data)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(data);
            var hex = BitConverter.ToString(hash);
            return hex.Replace("-", "");
        }

        private static byte[] Combine(byte[] primaryBytes, byte[] secondaryBytes)
        {
            var bytes = new byte[primaryBytes.Length + secondaryBytes.Length];
            Buffer.BlockCopy(primaryBytes, 0, bytes, 0, primaryBytes.Length);
            Buffer.BlockCopy(secondaryBytes, 0, bytes, primaryBytes.Length, secondaryBytes.Length);
            return bytes;
        }
    }
}