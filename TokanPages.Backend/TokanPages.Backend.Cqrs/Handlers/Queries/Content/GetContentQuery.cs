namespace TokanPages.Backend.Cqrs.Handlers.Queries.Content;

using MediatR;

public class GetContentQuery : IRequest<GetContentQueryResult>
{
    public string Type { get; set; }
        
    public string Name { get; set; }
        
    public string Language { get; set; }
}