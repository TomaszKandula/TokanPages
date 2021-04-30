using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Shared.Dto.Articles;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using MediatR;

namespace TokanPages.Controllers
{
    public class ArticlesController : __BaseController
    {
        public ArticlesController(IMediator AMediator) : base(AMediator) { }

        [HttpGet]
        public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool AIsPublished = true) 
            => await FMediator.Send(new GetAllArticlesQuery { IsPublished = AIsPublished });

        [HttpGet("{AId}")]
        public async Task<GetArticleQueryResult> GetArticle([FromRoute] Guid AId)
            => await FMediator.Send(new GetArticleQuery { Id = AId});

        [HttpPost]
        public async Task<Guid> AddArticle([FromBody] AddArticleDto APayLoad) 
            => await FMediator.Send(ArticlesMapper.MapToAddArticleCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> UpdateArticle([FromBody] UpdateArticleDto APayLoad)
            => await FMediator.Send(ArticlesMapper.MapToUpateArticleCommand(APayLoad));

        [HttpPost]
        public async Task<Unit> RemoveArticle([FromBody] RemoveArticleDto APayLoad)
            => await FMediator.Send(ArticlesMapper.MapToRemoveArticleCommand(APayLoad));
    }
}
