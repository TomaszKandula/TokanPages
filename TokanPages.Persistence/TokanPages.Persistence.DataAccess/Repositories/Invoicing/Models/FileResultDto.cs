using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class FileResultDto
{
    public required byte[] ContentData { get; init; }

    public required string ContentType { get; init; }
}