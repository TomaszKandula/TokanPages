using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.SmtpClient.Settings
{
    [ExcludeFromCodeCoverage]
    public class SmtpServerSettings
    {
        public string Account { get; set; }
        
        public string Password { get; set; }
        
        public int Port { get; set; }
        
        public string Server { get; set; }

        public bool IsSSL { get; set; }
    }
}
