﻿using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.SmtpClient.Models
{
    [ExcludeFromCodeCoverage]
    public class SmtpServerSettingsModel
    {
        public string Account { get; set; }
        
        public string Password { get; set; }
        
        public int Port { get; set; }
        
        public string Server { get; set; }

        public bool IsSSL { get; set; }
    }
}
