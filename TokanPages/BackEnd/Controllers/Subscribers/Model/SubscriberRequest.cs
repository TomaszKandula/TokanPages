using System;
using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Subscribers.Model
{

    public class SubscriberRequest
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

    }

}
