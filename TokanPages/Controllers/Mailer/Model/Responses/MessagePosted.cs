using System.Text.Json.Serialization;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Controllers.Mailer.Model.Responses
{
    
    public class MessagePosted
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public MessagePosted() 
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
