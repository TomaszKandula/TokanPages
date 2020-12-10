using System.Text.Json.Serialization;

namespace TokanPages.Controllers.Subscribers.Model.Responses
{

    public class ReturnSubscriber
    {

        [JsonPropertyName("subscriber")]
        public SubscriberItem Subscriber { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ReturnSubscriber() 
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
