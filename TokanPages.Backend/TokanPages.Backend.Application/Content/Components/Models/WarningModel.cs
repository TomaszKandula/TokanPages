using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

[ExcludeFromCodeCoverage]
public class WarningModel
{
    public string Language { get; set; } = "";

    public string Caption { get; set; } = "";

    public string Text1 { get; set; } = "";

    public string Text2 { get; set; } = "";

    public string Text3 { get; set; } = "";
}