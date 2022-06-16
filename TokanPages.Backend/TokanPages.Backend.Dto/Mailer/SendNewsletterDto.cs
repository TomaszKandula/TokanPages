namespace TokanPages.Backend.Dto.Mailer;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Models;

/// <summary>
/// Use it when you want to send newsletter to many subscribers
/// </summary>
[ExcludeFromCodeCoverage]
public class SendNewsletterDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public List<SubscriberInfo>? SubscriberInfo { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public string? Message { get; set; }
}