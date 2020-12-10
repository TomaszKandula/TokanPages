using System.Text.Json.Serialization;

namespace TokanPages.Backend.Shared.Models
{

    public class MetaData
    {

        [JsonPropertyName("rowsAffected")]
        public int RowsAffected { get; set; }

        [JsonPropertyName("requestedPage")]
        public int RequestedPage { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("processingTimeSpan")]
        public string ProcessingTimeSpan { get; set; }

        [JsonPropertyName("requesterIpAddress")]
        public string RequesterIpAddress { get; set; }

        public MetaData()
        {
            RowsAffected = 0;
            RequestedPage = 0;
            TotalPages = 0;
            ProcessingTimeSpan = "n/a";
            RequesterIpAddress = "0.0.0.0";
        }

    }

}
