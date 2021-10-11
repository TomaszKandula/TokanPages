namespace TokanPages.Backend.Shared.Dto.Content
{
    using Base;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ArticleFeaturesDto : BaseClass
    {
        public string Title { get; set; }

        public string Desc { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Button { get; set; }

        public string Image1 { get; set; }
        
        public string Image2 { get; set; }
        
        public string Image3 { get; set; }
        
        public string Image4 { get; set; }
    }
}