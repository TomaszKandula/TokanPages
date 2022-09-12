namespace TokanPages.Backend.Application.Handlers.Commands.Articles;

using System;
using MediatR;

public class UpdateArticleContentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string? Title { get; set; }
        
    public string? Description { get; set; }
        
    public string? TextToUpload { get; set; }
        
    public string? ImageToUpload { get; set; }
}