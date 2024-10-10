using MediatR;
using TokanPages.Backend.Application.Content.Components.Models;

namespace TokanPages.Backend.Application.Content.Components.Queries;

public class GetContentQuery : ContentModel, IRequest<GetContentQueryResult>
{
    public string? Language { get; set; }
}
