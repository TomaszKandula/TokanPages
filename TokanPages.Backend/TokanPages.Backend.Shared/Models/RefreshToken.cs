namespace TokanPages.Backend.Shared.Models
{
    using System;

    public class RefreshToken
    {
        public string Token { get; set; }
        
        public DateTime Expires { get; set; }
        
        public DateTime Created { get; set; }
        
        public string CreatedByIp { get; set; }
    }
}