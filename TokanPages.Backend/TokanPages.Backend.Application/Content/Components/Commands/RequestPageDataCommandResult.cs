using TokanPages.Backend.Application.Content.Components.Queries;

namespace TokanPages.Backend.Application.Content.Components.Commands;

public class RequestPageDataCommandResult
{
    public List<GetContentQueryResult> Components { get; set; } = new();

    public string? PageName { get; set; }

    public string? Language { get; set; }
}