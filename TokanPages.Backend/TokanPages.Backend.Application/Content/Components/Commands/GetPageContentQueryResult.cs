using TokanPages.Backend.Application.Content.Components.Queries;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class GetPageContentQueryResult
{
    public List<GetContentQueryResult> Components { get; set; } = new();

    public string? Language { get; set; }
}