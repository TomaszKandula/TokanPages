using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.SmtpClient.Models
{
    [ExcludeFromCodeCoverage]
    public class CheckActionResult
    {
        public string EmailAddress { get; set; }

        public bool IsValid { get; set; }
    }
}
