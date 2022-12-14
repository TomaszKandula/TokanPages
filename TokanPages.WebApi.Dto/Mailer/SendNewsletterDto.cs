using System.Diagnostics.CodeAnalysis;
using TokanPages.WebApi.Dto.Mailer.Models;

namespace TokanPages.WebApi.Dto.Mailer;

/// <summary>
/// Use it when you want to send newsletter to many subscribers.
/// </summary>
[ExcludeFromCodeCoverage]
public class SendNewsletterDto
{
    /// <summary>
    /// List of subscriber details.
    /// </summary>
    public List<SubscriberInfo> SubscriberInfo { get; set; } = new();

    /// <summary>
    /// Subject field.
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// Message content.
    /// </summary>
    public string Message { get; set; } = "";
}