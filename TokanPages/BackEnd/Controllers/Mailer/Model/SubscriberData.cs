using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Mailer.Model
{

    public class SubscriberData
    {

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

    }

}
