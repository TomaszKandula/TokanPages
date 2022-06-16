namespace TokanPages.Backend.Shared.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class EmailSender
{
    public string PrivateKey { get; set; } = "";

    public string BaseUrl { get; set; } = "";
}