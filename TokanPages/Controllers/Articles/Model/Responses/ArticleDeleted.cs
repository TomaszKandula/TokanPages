using System.Text.Json.Serialization;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Controllers.Articles.Model.Responses
{

    public class ArticleDeleted
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ArticleDeleted()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
