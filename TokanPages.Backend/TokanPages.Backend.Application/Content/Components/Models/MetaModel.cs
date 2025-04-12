using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

[ExcludeFromCodeCoverage]
public class MetaModel
{
    public string Language { get; set; } = "";

    public string Description { get; set; } = "";

    public FacebookModel Facebook { get; set; } = new();

    public TwitterModel Twitter { get; set; } = new();
}