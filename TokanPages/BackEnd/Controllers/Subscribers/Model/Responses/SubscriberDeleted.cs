using System.Text.Json.Serialization;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Controllers.Subscribers.Model.Responses
{
 
    public class SubscriberDeleted
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public SubscriberDeleted()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
