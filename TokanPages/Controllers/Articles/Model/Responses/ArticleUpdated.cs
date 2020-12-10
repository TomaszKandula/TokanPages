using System.Text.Json.Serialization;

namespace TokanPages.Controllers.Articles.Model.Responses
{

    public class ArticleUpdated
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ArticleUpdated()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
