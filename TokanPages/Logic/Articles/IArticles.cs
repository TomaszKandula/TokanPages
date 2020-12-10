using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Controllers.Articles.Model;

namespace TokanPages.Logic.Articles 
{

    public interface IArticles 
    {
        Task<List<ArticleItem>> GetAllArticles();
        Task<ArticleItem> GetSingleArticle(Guid Id);
        Task<Guid> AddNewArticle(ArticleRequest PayLoad);
        Task<HttpStatusCode> UpdateArticle(ArticleRequest PayLoad);
        Task<HttpStatusCode> DeleteArticle(Guid Id);
    }

}
