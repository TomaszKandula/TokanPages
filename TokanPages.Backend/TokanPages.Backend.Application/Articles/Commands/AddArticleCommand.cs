namespace TokanPages.Backend.Application.Articles.Commands;

using System;
using MediatR;

public class AddArticleCommand : IRequest<Guid>
{
    public string? Title { get; set; }

    public string? Description { get; set; }
        
    public string? TextToUpload { get; set; }
        
    public string? ImageToUpload { get; set; }
}