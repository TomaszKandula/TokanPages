using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.BatchService.Models;

[ExcludeFromCodeCoverage]
public class FileResult
{
    public byte[] ContentData { get; set; } = Array.Empty<byte>();

    public string ContentType { get; set; } = "";
}