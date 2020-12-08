using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Mailer.Model
{

    public class SendNewsletter
    {

        [JsonPropertyName("subscribersData")]
        public List<SubscriberData> SubscribersData { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

    }

}
