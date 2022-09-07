namespace TokanPages.WebApi.Dto.Mailer;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Payload object for email sender
/// </summary>
[ExcludeFromCodeCoverage]
public class SenderPayloadDto
{
    /// <summary>
    /// From
    /// </summary>
    public string From { get; set; } = "";

    /// <summary>
    /// To
    /// </summary>
    public IEnumerable<string>? To { get; set; }

    /// <summary>
    /// Cc
    /// </summary>
    public IEnumerable<string>? Cc { get; set; }

    /// <summary>
    /// Bcc
    /// </summary>
    public IEnumerable<string>? Bcc { get; set; }

    /// <summary>
    /// Subject
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// Body
    /// </summary>
    public string Body { get; set; } = "";

    /// <summary>
    /// IsHtml
    /// </summary>
    public bool IsHtml { get; set; } = true;
}