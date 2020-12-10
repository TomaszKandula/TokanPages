using System;
using System.Text.Json.Serialization;

namespace TokanPages.Controllers.Subscribers.Model.Responses
{

    public class SubscriberAdded
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("newUid")]
        public Guid NewUid { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public SubscriberAdded() 
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
