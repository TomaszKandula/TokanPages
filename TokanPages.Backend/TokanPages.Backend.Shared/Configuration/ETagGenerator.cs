using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace TokanPages.Backend.Shared.Configuration;

/// <summary>
/// Docker Support
/// </summary>
[ExcludeFromCodeCoverage]
public static class ETagGenerator
{
    /// <summary>
    /// Get new ETag
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="contentBytes">Content</param>
    /// <returns></returns>
    public static string GetETag(string key, byte[] contentBytes)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var combinedBytes = Combine(keyBytes, contentBytes);

        return GenerateETag(combinedBytes);
    }

    private static string GenerateETag(byte[] data)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(data);
        var hex = BitConverter.ToString(hash);
        return hex.Replace("-", "");
    }

    private static byte[] Combine(byte[] bytes1, byte[] bytes2)
    {
        var combineBytes = new byte[bytes1.Length + bytes2.Length];
        Buffer.BlockCopy(bytes1, 0, combineBytes, 0, bytes1.Length);
        Buffer.BlockCopy(bytes2, 0, combineBytes, bytes1.Length, bytes2.Length);
        return combineBytes;
    }
}