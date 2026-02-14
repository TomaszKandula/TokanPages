using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class FileResultDto
{
    public byte[] ContentData { get; init; } = Array.Empty<byte>();

    public string ContentType { get; init; } = string.Empty;
}