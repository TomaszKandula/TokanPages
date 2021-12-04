namespace TokanPages.WebApi.Services.Caching.Content
{
    using System.Threading.Tasks;
    using Backend.Cqrs.Handlers.Queries.Content;

    public interface IContentCache
    {
        Task<GetContentQueryResult> GetContent(string type, string name, string language, bool noCache = false);
    }
}