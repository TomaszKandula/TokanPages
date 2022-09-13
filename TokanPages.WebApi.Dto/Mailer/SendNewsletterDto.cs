using System.Diagnostics.CodeAnalysis;
using TokanPages.WebApi.Dto.Mailer.Models;

namespace TokanPages.WebApi.Dto.Mailer;

/// <summary>
/// Use it when you want to send newsletter to many subscribers
/// </summary>
[ExcludeFromCodeCoverage]
public class SendNewsletterDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public List<SubscriberInfo> SubscriberInfo { get; set; } = new();

    /// <summary>
    /// Mandatory
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// Mandatory
    /// </summary>
    public string Message { get; set; } = "";
}