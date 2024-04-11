using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.EmailSenderService.Abstractions;

namespace TokanPages.Services.EmailSenderService.Models;

[ExcludeFromCodeCoverage]
public class SendMessageConfiguration : IEmailConfiguration
{
    public Guid MessageId { get; set; }

    public string From { get; set; } = "";

    public List<string> To { get; set; } = new();

    public List<string>? Cc { get; set; }

    public List<string>? Bcc { get; set; }

    public string Subject { get; set; } = "";

    public string Body { get; set; } = "";

    public bool IsHtml { get; set; } = true;
}