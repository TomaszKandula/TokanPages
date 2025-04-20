using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Logger.Commands.Models;

[ExcludeFromCodeCoverage]
public class Device
{
    public string? Model { get; set; }

    public string? Type { get; set; }

    public string? Vendor { get; set; }
}