using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class LanguageItemDto
{
    public string Id { get; set; } = string.Empty;

    public string Iso { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public bool IsDefault { get; set; }
}