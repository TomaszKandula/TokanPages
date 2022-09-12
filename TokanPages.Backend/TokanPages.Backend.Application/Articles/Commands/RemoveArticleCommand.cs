namespace TokanPages.Backend.Application.Articles.Commands;

using System;
using MediatR;

public class RemoveArticleCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}