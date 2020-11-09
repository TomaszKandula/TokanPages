using System.Text.Json.Serialization;

namespace TokanPages.BackEnd.Shared
{

    public class ErrorHandler
    {

        [JsonPropertyName("errorDesc")]
        public string ErrorDesc { get; set; }

        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        public ErrorHandler()
        {
            ErrorDesc = "n/a";
            ErrorCode = "no_errors_found";
        }

    }

}
