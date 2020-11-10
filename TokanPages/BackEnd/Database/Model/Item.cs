using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Database.Model
{
    
    public class Item
    {

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("likes")]
        public int Likes { get; set; }

    }

}
