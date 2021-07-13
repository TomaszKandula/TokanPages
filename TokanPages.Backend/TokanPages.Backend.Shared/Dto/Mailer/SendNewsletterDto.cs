namespace TokanPages.Backend.Shared.Dto.Mailer
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Models;

    [ExcludeFromCodeCoverage]
    public class SendNewsletterDto
    {
        public List<SubscriberInfoModel> SubscriberInfo { get; set; }

        public string Subject { get; set; }
        
        public string Message { get; set; }
    }
}