using TokanPages.Backend.Application.Content.Components.Queries;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class GetPageContentCommandResult
{
    public List<GetContentQueryResult> Components { get; set; } = new();

    public string? Language { get; set; }
}