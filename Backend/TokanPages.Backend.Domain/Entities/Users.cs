using System;
using System.Text.Json.Serialization;

namespace TokanPages.Backend.Domain.Entities
{

    public class Users
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("userAlias")]
        public string UserAlias { get; set; }

        [JsonPropertyName("userStatus")]
        public string UserStatus { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("registered")]
        public DateTime Registered { get; set; }

        [JsonPropertyName("lastLogged")]
        public DateTime LastLogged { get; set; }

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

    }

}
