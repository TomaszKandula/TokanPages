using System.Text.Json.Serialization;
using TokanPages.BackEnd.Shared;

namespace TokanPages.BackEnd.Controllers.Articles.Model.Responses
{

    public class ReturnArticle
    {

        [JsonPropertyName("articles")]
        public ArticleItem Articles { get; set; }

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
