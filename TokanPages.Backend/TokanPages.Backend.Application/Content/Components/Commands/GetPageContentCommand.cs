using MediatR;
using TokanPages.Backend.Application.Content.Components.Models;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class GetPageContentCommand : IRequest<GetPageContentCommandResult>
{
    public List<ContentModel> Components { get; set; } = new();

    public string? Language { get; set; }
}
