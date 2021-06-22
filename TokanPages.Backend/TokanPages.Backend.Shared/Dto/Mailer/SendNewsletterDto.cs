using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Backend.Shared.Dto.Mailer
{
    [ExcludeFromCodeCoverage]
    public class SendNewsletterDto
    {
        public List<SubscriberInfoModel> SubscriberInfo { get; set; }

        public string Subject { get; set; }
        
        public string Message { get; set; }
    }
}
