using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Articles.Model
{

    public class ArticleItem
    {

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

    }

}
