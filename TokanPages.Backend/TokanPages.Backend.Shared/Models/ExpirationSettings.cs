namespace TokanPages.Backend.Shared.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ExpirationSettings
    {
        public int ResetIdExpiresIn { get; set; }
        
        public int ActivationIdExpiresIn { get; set; }
    }
}