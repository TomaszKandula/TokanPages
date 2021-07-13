namespace TokanPages.Backend.Shared.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ApplicationPathsModel
    {
        public string UpdateSubscriberPath { get; set; }

        public string UnsubscribePath { get; set; }
        
        public string DevelopmentOrigin { get; set; }
        
        public string DeploymentOrigin { get; set; }
    }
}