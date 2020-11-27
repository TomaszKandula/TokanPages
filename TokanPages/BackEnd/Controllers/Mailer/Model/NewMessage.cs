using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Mailer.Model
{

    public class NewMessage
    {

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }

        [JsonPropertyName("emailFrom")]
        public string EmailFrom { get; set; }

        [JsonPropertyName("subscribersData")]
        public List<SubscriberData> SubscribersData { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

    }

}
