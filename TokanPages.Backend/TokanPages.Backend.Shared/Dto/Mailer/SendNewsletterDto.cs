using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Backend.Shared.Dto.Mailer
{
    public class SendNewsletterDto
    {
        public List<SubscriberInfo> SubscriberInfo { get; set; }

        public string Subject { get; set; }
        
        public string Message { get; set; }
    }
}
