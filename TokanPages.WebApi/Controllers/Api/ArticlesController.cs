namespace TokanPages.WebApi.Controllers.Api
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Cqrs.Mappers;
    using Backend.Shared.Dto.Articles;
    using Backend.Identity.Attributes;
    using Backend.Identity.Authorization;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using MediatR;
    
    [Authorize]
    public class ArticlesController : ApiBaseController
    {
        public ArticlesController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool isPublished = true) 
            => await Mediator.Send(new GetAllArticlesQuery { IsPublished = isPublished });

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<GetArticleQueryResult> GetArticle([FromRoute] Guid id)
            => await Mediator.Send(new GetArticleQuery { Id = id});

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Guid> AddArticle([FromBody] AddArticleDto payLoad) 
            => await Mediator.Send(ArticlesMapper.MapToAddArticleCommand(payLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
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
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> UpdateArticleVisibility([FromBody] UpdateArticleVisibilityDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payLoad));

        [HttpPost]
        [AuthorizeRoles(Roles.GodOfAsgard, Roles.EverydayUser)]
        public async Task<Unit> RemoveArticle([FromBody] RemoveArticleDto payLoad)
            => await Mediator.Send(ArticlesMapper.MapToRemoveArticleCommand(payLoad));
    }
}