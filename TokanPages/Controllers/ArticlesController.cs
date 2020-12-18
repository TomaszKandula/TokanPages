using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Cqrs.Mappers;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Dto.Articles;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using MediatR;

namespace TokanPages.Controllers
{

    public class ArticlesController : __BaseController
    {

        public ArticlesController(IMediator AMediator) : base(AMediator)
        {        
        }

        [HttpGet]
        public async Task<IEnumerable<Articles>> GetAllArticles() 
        {
            var LQuery = new GetAllArticlesQuery();
            return await FMediator.Send(LQuery);
        }

        [HttpGet]
        public async Task<Articles> GetArticle([FromRoute] Guid Id)
        {
            var LQuery = new GetArticleQuery { Id = Id};
            return await FMediator.Send(LQuery);
        }

        [HttpPost]
        public async Task<Unit> AddArticle([FromBody] AddArticleRequest APayLoad) 
        {
            var LCommand = ArticlesMapper.MapToAddArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> UpdateArticle([FromBody] UpdateArticleRequest APayLoad)
        {
            var LCommand = ArticlesMapper.MapToUpateArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> RemoveArticle([FromBody] RemoveArticleRequest APayLoad)
        {
            var LCommand = ArticlesMapper.MapToRemoveArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

    }

}
