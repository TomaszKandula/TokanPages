using System;
using System.Text.Json.Serialization;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Controllers.Articles.Model.Responses
{

    public class ArticleAdded
    {

        [JsonPropertyName("isSucceeded")]
        public bool IsSucceeded { get; set; }

        [JsonPropertyName("newUid")]
        public Guid NewUid { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public ArticleAdded()
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
