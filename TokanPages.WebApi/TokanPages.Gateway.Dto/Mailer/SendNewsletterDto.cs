using System.Diagnostics.CodeAnalysis;
using TokanPages.Gateway.Dto.Mailer.Models;

namespace TokanPages.Gateway.Dto.Mailer;

/// <summary>
/// Use it when you want to send newsletter to many subscribers.
/// </summary>
[ExcludeFromCodeCoverage]
public class SendNewsletterDto
{
    /// <summary>
    /// List of subscriber details.
    /// </summary>
    public IEnumerable<SubscriberInfo>? SubscriberInfo { get; set; }

    /// <summary>
    /// Subject field.
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// Message content.
    /// </summary>
    public string Message { get; set; } = "";
}