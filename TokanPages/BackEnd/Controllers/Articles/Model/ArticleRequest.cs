using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Controllers.Articles.Model
{

    public class ArticleRequest
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("likes")]
        public int Likes { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

    }

}
