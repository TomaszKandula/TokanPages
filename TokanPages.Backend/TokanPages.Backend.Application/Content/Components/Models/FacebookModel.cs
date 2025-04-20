using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

[ExcludeFromCodeCoverage]
public class FacebookModel : TwitterModel
{
    public string ImageAlt { get; set; } = "";
}