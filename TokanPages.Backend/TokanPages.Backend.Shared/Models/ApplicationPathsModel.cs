using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Models
{
    [ExcludeFromCodeCoverage]
    public class ApplicationPathsModel
    {
        public string UpdateSubscriberPath { get; set; }

        public string UnsubscribePath { get; set; }
        
        public string DevelopmentOrigin { get; set; }
        
        public string DeploymentOrigin { get; set; }
    }
}
