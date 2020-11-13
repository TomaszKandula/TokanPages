using System.Text.Json.Serialization;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Controllers.Mailer.Model.Responses
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
