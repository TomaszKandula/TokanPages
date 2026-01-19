using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class AddArticleCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string TextToUpload { get; set; } = string.Empty;

    public string ImageToUpload { get; set; } = string.Empty;

    public string LanguageIso { get; set; } = "ENG";
}