using System.Text.Json.Serialization;

namespace TokanPages.Backend.Shared.Models
{

    public class ErrorHandler
    {

        [JsonPropertyName("errorDesc")]
        public string ErrorDesc { get; set; }

        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; }

        public ErrorHandler()
        {
            ErrorCode = Constants.Errors.Default.ErrorCode;
            ErrorDesc = Constants.Errors.Default.ErrorDesc;
        }

    }

}
