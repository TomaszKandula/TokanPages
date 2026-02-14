using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class LanguageItemDto
{
    public required string Id { get; init; }

    public required string Iso { get; init; }

    public required string Name { get; init; }

    public required bool IsDefault { get; init; }
}