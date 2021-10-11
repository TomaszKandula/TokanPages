namespace TokanPages.Backend.Shared.Dto.Content.Common
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ContentActivation
    {
        public string Type { get; set; }
        
        public string Caption { get; set; }
        
        public string Text1 { get; set; }
        
        public string Text2 { get; set; }
        
        public string Button { get; set; }
    }
}