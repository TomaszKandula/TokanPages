using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.SmtpClient.Models
{
    [ExcludeFromCodeCoverage]
    public class EmailAddressModel
    {
        public string EmailAddress { get; set; }

        public bool IsValid { get; set; }
    }
}
