namespace TokanPages.Backend.Application.Articles.Commands;

using System;
using MediatR;

public class UpdateArticleVisibilityCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
        
    public bool IsPublished { get; set; }
}