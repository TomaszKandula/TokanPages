using System;
using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Database.Model
{

    public class Subscribers
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("newsletterCount")]
        public int NewsletterCount { get; set; }

        [JsonPropertyName("registered")]
        public DateTime Registered { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

    }

}
