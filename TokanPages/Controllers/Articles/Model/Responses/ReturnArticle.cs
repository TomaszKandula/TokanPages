using System.Text.Json.Serialization;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Controllers.Articles.Model.Responses
{

    public class ReturnArticle
    {

        [JsonPropertyName("article")]
        public ArticleItem Article { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ReturnArticle()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
