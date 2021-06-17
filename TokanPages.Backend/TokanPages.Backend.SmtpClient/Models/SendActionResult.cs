using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.SmtpClient.Models
{
    [ExcludeFromCodeCoverage]
    public class SendActionResult
    {
        public bool IsSucceeded { get; set; }

        public string ErrorCode { get; set; }
        
        public string ErrorDesc { get; set; } = "n/a";
    }
}
