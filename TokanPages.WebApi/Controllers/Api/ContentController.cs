namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Handlers.Queries.Content;
    using Services.Caching.Content;
    using MediatR;

    public class ContentController : ApiBaseController
    {
        private readonly IContentCache _contentCache;

        public ContentController(IMediator mediator, IContentCache contentCache) 
            : base(mediator) => _contentCache = contentCache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<GetContentQueryResult> GetContent([FromQuery] string type, string name, string language, bool noCache = false)
            => await _contentCache.GetContent(type, name, language, noCache);
    }
}