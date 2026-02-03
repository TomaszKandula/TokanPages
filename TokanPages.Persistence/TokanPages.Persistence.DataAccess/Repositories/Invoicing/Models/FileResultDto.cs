using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class FileResultDto
{
    public byte[] ContentData { get; set; } = Array.Empty<byte>();

    public string ContentType { get; set; } = string.Empty;
}