using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Services.Models;

[ExcludeFromCodeCoverage]
public class Templates
{
    public string Newsletter { get; set; } = "";

    public string ContactForm { get; set; } = "";

    public string ResetPassword { get; set; } = "";

    public string RegisterForm { get; set; } = "";
}