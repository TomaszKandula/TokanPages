using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Logger.Commands.Models;

[ExcludeFromCodeCoverage]
public class Engine
{
    public string? Name { get; set; }

    public string? Version { get; set; }
}