namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Handlers.Queries.Content;
    using MediatR;
    
    public class ContentController : ApiBaseController
    {
        public ContentController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [AllowAnonymous]
        public async Task<GetContentQueryResult> GetContent([FromQuery] string type, string name, string language)
            => await Mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });
    }
}