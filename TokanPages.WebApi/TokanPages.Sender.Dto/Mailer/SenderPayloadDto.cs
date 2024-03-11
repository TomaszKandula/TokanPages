using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Sender.Dto.Mailer;

/// <summary>
/// Payload object for email sender.
/// </summary>
[ExcludeFromCodeCoverage]
public class SenderPayloadDto
{
    /// <summary>
    /// From field.
    /// </summary>
    public string From { get; set; } = "";

    /// <summary>
    /// To field.
    /// </summary>
    public IEnumerable<string>? To { get; set; }

    /// <summary>
    /// Cc field.
    /// </summary>
    public IEnumerable<string>? Cc { get; set; }

    /// <summary>
    /// Bcc field.
    /// </summary>
    public IEnumerable<string>? Bcc { get; set; }

    /// <summary>
    /// Subject field.
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// Body field.
    /// </summary>
    public string Body { get; set; } = "";

    /// <summary>
    /// IsHtml flag.
    /// </summary>
    public bool IsHtml { get; set; } = true;
}