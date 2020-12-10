using System.Collections.Generic;
using System.Text.Json.Serialization;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Controllers.Articles.Model.Responses
{

    public class ReturnArticles
    {

        [JsonPropertyName("articles")]
        public List<ArticleItem> Articles { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ReturnArticles()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
