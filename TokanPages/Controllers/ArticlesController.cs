using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Domain.Entities;
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
            var LQuery = new GetAllArticlesCommand();
            return await FMediator.Send(LQuery);
        }

        [HttpGet]
        public async Task<Articles> GetArticle()
        {
            var LQuery = new GetArticleCommand();
            return await FMediator.Send(LQuery);
        }

        

    }

}
