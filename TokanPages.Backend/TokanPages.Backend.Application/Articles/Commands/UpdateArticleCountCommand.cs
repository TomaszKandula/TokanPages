namespace TokanPages.Backend.Application.Articles.Commands;

using System;
using MediatR;

public class UpdateArticleCountCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}