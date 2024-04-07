using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.TemplateService.Models;

[ExcludeFromCodeCoverage]
public class FileResult
{
    public byte[] ContentData { get; set; } = Array.Empty<byte>();

    public string ContentType { get; set; } = "";
}