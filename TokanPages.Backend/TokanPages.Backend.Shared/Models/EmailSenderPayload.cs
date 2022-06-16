namespace TokanPages.Backend.Shared.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class EmailSenderPayload
{
    public string From { get; set; } = "";

    public IEnumerable<string> To { get; set; } = new List<string>();

    public IEnumerable<string> Cc { get; set; } = new List<string>();

    public IEnumerable<string> Bcc { get; set; } = new List<string>();

    public string Subject { get; set; } = "";

    public string Body { get; set; } = "";

    public bool IsHtml { get; set; } = true;
}