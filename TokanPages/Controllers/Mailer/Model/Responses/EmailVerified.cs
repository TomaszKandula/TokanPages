using System.Text.Json.Serialization;

namespace TokanPages.Controllers.Mailer.Model.Responses
{
    public class EmailVerified
    {

        [JsonPropertyName("isFormatCorrect")]
        public bool IsFormatCorrect { get; set; }

        [JsonPropertyName("isDomainCorrect")]
        public bool IsDomainCorrect { get; set; }

        [JsonPropertyName("error")]
        public ErrorHandler Error { get; set; }

        [JsonPropertyName("meta")]
        public MetaData Meta { get; set; }

        public EmailVerified() 
        {
            Error = new ErrorHandler();
            Meta = new MetaData();
        }

    }

}
