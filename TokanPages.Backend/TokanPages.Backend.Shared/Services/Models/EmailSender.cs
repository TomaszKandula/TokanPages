using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Services.Models;

[ExcludeFromCodeCoverage]
public class EmailSender
{
    public string PrivateKey { get; set; } = "";

    public string BaseUrl { get; set; } = "";

    public Addresses Addresses { get; set; } = new();
}