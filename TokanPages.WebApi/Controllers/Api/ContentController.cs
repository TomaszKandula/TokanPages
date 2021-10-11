namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Handlers.Queries.Content;
    using MediatR;
    
    public class ContentController : ApiBaseController
    {
        public ContentController(IMediator AMediator) : base(AMediator) { }

        [HttpGet]
        [AllowAnonymous]
        public async Task<GetContentQueryResult> GetContent([FromQuery] string AType, string AName, string ALanguage)
            => await FMediator.Send(new GetContentQuery { Type = AType, Name = AName, Language = ALanguage });
    }
}