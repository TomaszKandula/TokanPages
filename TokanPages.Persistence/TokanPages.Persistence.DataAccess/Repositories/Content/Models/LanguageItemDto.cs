using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class LanguageItemDto
{
    public required string Id { get; init; } = string.Empty;

    public required string Iso { get; init; } = string.Empty;

    public required string Name { get; init; } = string.Empty;

    public required bool IsDefault { get; init; }
}