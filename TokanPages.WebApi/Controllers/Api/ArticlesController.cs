namespace TokanPages.WebApi.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Attributes;
    using Backend.Cqrs.Mappers;
    using Backend.Shared.Dto.Articles;
    using Backend.Identity.Authorization;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using Services.Caching.Articles;
    using MediatR;

    [Authorize]
    public class ArticlesController : ApiBaseController
    {
        private readonly IArticlesCache _articlesCache;

        public ArticlesController(IMediator mediator, IArticlesCache articlesCache) 
            : base(mediator) => _articlesCache = articlesCache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool isPublished = true, bool noCache = false)
            => await _articlesCache.GetArticles(isPublished, noCache);

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<GetArticleQueryResult> GetArticle([FromRoute] Guid id, [FromQuery] bool noCache = false)
            => await _articlesCache.GetArticle(id, noCache);

        [HttpPost]
        [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Guid> AddArticle([FromBody] AddArticleDto payLoad) 
            => await Mediator.Send(ArticlesMapper.MapToAddArticleCommand(payLoad));

        [HttpPost]
        [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> UpdateArticleContent([FromBody] UpdateArticleContentDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> UpdateArticleCount([FromBody] UpdateArticleCountDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payLoad));

        [HttpPost]
        [AllowAnonymous]
        public async Task<Unit> UpdateArticleLikes([FromBody] UpdateArticleLikesDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payLoad));

        [HttpPost]
        [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> UpdateArticleVisibility([FromBody] UpdateArticleVisibilityDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payLoad));

        [HttpPost]
        [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> RemoveArticle([FromBody] RemoveArticleDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToRemoveArticleCommand(payLoad));
    }
}