namespace TokanPages.Backend.SmtpClient.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class EmailAddressModel
    {
        public string EmailAddress { get; set; }

        public bool IsValid { get; set; }
    }
}