using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Mailer.Model
{

    public class NewMessage
    {

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("emailFrom")]
        public string EmailFrom { get; set; }

        [JsonPropertyName("emailTo")]
        public string EmailTo { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

    }

}
