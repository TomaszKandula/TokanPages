namespace TokanPages.Backend.Shared.Attributes
{
    using System;
    using System.Text;
    using System.Security.Cryptography;

    public static class ETagGenerator
    {
        public static string GetETag(string AKey, byte[] AContentBytes)
        {
            var LKeyBytes = Encoding.UTF8.GetBytes(AKey);
            var LCombinedBytes = Combine(LKeyBytes, AContentBytes);
            return GenerateETag(LCombinedBytes);
        }

        private static string GenerateETag(byte[] AData)
        {
            using var LSha256 = SHA256.Create();
            var LHash = LSha256.ComputeHash(AData);
            var LHex = BitConverter.ToString(LHash);
            return LHex.Replace("-", "");
        }

        private static byte[] Combine(byte[] APrimaryBytes, byte[] ASecondaryBytes)
        {
            var LBytes = new byte[APrimaryBytes.Length + ASecondaryBytes.Length];
            Buffer.BlockCopy(APrimaryBytes, 0, LBytes, 0, APrimaryBytes.Length);
            Buffer.BlockCopy(ASecondaryBytes, 0, LBytes, APrimaryBytes.Length, ASecondaryBytes.Length);
            return LBytes;
        }
    }
}