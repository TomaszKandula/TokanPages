namespace TokanPages.Backend.Shared.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ApplicationPaths
    {
        public string UpdateSubscriberPath { get; set; }

        public string UnsubscribePath { get; set; }
        
        public string ResetPath { get; set; }
        
        public string DevelopmentOrigin { get; set; }
        
        public string DeploymentOrigin { get; set; }
    }
}