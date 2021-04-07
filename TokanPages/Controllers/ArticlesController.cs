﻿using System;
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
        {
            var LQuery = new GetAllArticlesQuery { IsPublished = AIsPublished };
            return await FMediator.Send(LQuery);
        }

        [HttpGet("{AId}")]
        public async Task<GetArticleQueryResult> GetArticle([FromRoute] Guid AId)
        {
            var LQuery = new GetArticleQuery { Id = AId};
            return await FMediator.Send(LQuery);
        }

        [HttpPost]
        public async Task<Guid> AddArticle([FromBody] AddArticleDto APayLoad) 
        {
            var LCommand = ArticlesMapper.MapToAddArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> UpdateArticle([FromBody] UpdateArticleDto APayLoad)
        {
            var LCommand = ArticlesMapper.MapToUpateArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpPost]
        public async Task<Unit> RemoveArticle([FromBody] RemoveArticleDto APayLoad)
        {
            var LCommand = ArticlesMapper.MapToRemoveArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }
    }
}
