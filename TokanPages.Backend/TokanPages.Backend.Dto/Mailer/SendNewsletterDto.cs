namespace TokanPages.Backend.Dto.Mailer;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Models;

[ExcludeFromCodeCoverage]
public class SendNewsletterDto
{
    public List<SubscriberInfo> SubscriberInfo { get; set; }

    public string Subject { get; set; }
        
    public string Message { get; set; }
}