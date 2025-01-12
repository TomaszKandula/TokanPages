using System.IO.Compression;
using System.Text;

namespace TokanPages.Backend.Core.Extensions;

public static class CompressionExtensions
{
    public static string CompressToBase64(this string data)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(data).Compress());
    }

    public static string DecompressFromBase64(this string data)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(data).Decompress());
    }

    public static byte[] Compress(this byte[] data)
    {
        using var sourceStream = new MemoryStream(data);
        using var destinationStream = new MemoryStream();

        sourceStream.CompressTo(destinationStream);
        return destinationStream.ToArray();
    }

    public static byte[] Decompress(this byte[] data)
    {
        using var sourceStream = new MemoryStream(data);
        using var destinationStream = new MemoryStream();

        sourceStream.DecompressTo(destinationStream);
        return destinationStream.ToArray();
    }

    public static void CompressTo(this Stream stream, Stream outputStream)
    {
        using var gZipStream = new GZipStream(outputStream, CompressionMode.Compress);
        stream.CopyTo(gZipStream);
        gZipStream.Flush();
    }

    public static void DecompressTo(this Stream stream, Stream outputStream)
    {
        using var gZipStream = new GZipStream(stream, CompressionMode.Decompress);
        gZipStream.CopyTo(outputStream);
    }    
}