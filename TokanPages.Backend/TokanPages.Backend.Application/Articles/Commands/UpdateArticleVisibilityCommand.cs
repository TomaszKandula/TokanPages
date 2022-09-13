using System;
using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleVisibilityCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
        
    public bool IsPublished { get; set; }
}