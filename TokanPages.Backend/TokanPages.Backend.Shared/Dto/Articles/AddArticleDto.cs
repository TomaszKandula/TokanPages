namespace TokanPages.Backend.Shared.Dto.Articles;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AddArticleDto
{
    public string Title { get; set; }

    public string Description { get; set; }
        
    public string TextToUpload { get; set; }
        
    public string ImageToUpload { get; set; }
}