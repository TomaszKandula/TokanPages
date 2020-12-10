using System.Text.Json.Serialization;

namespace TokanPages.Controllers.Mailer.Model.Responses
{

    public class NewsletterPosted
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public NewsletterPosted()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
