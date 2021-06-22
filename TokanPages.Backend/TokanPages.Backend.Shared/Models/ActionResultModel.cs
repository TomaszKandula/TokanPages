using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Shared.Models
{
    [ExcludeFromCodeCoverage]
    public class ActionResultModel
    {
        public bool IsSucceeded { get; set; }

        public string ErrorCode { get; set; }
        
        public string ErrorDesc { get; set; }
        
        public string InnerMessage { get; set; }
    }
}
