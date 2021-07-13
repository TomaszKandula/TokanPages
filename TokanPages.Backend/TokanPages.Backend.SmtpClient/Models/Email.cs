namespace TokanPages.Backend.SmtpClient.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Email
    {
        public string Address { get; set; }

        public bool IsValid { get; set; }
    }
}