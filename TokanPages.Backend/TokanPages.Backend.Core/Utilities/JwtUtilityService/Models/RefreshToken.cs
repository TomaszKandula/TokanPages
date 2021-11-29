namespace TokanPages.Backend.Core.Utilities.JwtUtilityService.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class RefreshToken
    {
        public string Token { get; set; }
        
        public DateTime Expires { get; set; }
        
        public DateTime Created { get; set; }
        
        public string CreatedByIp { get; set; }
    }
}