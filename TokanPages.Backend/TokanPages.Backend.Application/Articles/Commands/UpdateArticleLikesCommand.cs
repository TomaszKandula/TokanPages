using System;
using MediatR;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleLikesCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
        
    public int AddToLikes { get; set; }
}