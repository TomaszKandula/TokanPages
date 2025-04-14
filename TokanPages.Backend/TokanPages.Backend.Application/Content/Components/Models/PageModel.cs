using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

[ExcludeFromCodeCoverage]
public class PageModel
{
    public string Page { get; set; } = "";

    public string Title {get; set;} = "";
}