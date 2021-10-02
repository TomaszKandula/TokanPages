namespace TokanPages.WebApi.Controllers.Api
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Cqrs.Handlers.Queries.Content;
    using MediatR;
    
    public class ContentController : BaseController
    {
        public ContentController(IMediator AMediator) : base(AMediator) { }

        [AllowAnonymous]
        public async Task<GetContentQueryResult> GetContent([FromQuery] string AType, string AName, string ALanguage)
            => await FMediator.Send(new GetContentQuery { Type = AType, Name = AName, Language = ALanguage });
    }
}