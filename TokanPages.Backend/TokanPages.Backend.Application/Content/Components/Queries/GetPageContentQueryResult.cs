using TokanPages.Backend.Application.Content.Components.Models;

namespace TokanPages.Backend.Application.Content.Components.Queries;

public class GetPageContentQueryResult
{
    public List<ContentModel> Components { get; set; } = new();
}