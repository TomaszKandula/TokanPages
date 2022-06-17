namespace TokanPages.Backend.Shared.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class EmailSenderPayload //TODO: relocate model
{
    public string From { get; set; } = "";

    public IEnumerable<string>? To { get; set; }

    public IEnumerable<string>? Cc { get; set; }

    public IEnumerable<string>? Bcc { get; set; }

    public string Subject { get; set; } = "";

    public string Body { get; set; } = "";

    public bool IsHtml { get; set; } = true;
}