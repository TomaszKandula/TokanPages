using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleContentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string? Title { get; set; }
        
    public string? Description { get; set; }
        
    public string? TextToUpload { get; set; }
        
    public string? ImageToUpload { get; set; }

    public string? LanguageIso { get; set; }
}