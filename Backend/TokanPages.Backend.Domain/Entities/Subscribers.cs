using System;
using System.Text.Json.Serialization;

namespace TokanPages.Backend.Domain.Entities
{

    public class Subscribers
    {

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("registered")]
        public DateTime Registered { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; set; }

    }

}
