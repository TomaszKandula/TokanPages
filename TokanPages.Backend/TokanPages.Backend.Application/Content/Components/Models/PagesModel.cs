using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

[ExcludeFromCodeCoverage]
public class PagesModel
{
    public string Language { get; set; } = "";

    public List<PageModel> Pages {get ; set; } = new();
}