namespace TokanPages.Backend.Application.Content.Queries;

using MediatR;

public class GetContentQuery : IRequest<GetContentQueryResult>
{
    public string Type { get; set; } = "";

    public string Name { get; set; } = "";

    public string? Language { get; set; }
}