using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Logger.Commands.Models;

[ExcludeFromCodeCoverage]
public class Browser
{
    public string? Major { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Version { get; set; }
}