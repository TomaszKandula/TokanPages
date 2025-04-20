using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

[ExcludeFromCodeCoverage]
public class ContentModel
{
    public string ContentName { get; set; } = "";

    public dynamic? Content { get; set; }
}