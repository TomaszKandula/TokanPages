namespace TokanPages.Backend.Application.Articles.Commands;

using System;
using MediatR;

public class UpdateArticleLikesCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
        
    public int AddToLikes { get; set; }
}