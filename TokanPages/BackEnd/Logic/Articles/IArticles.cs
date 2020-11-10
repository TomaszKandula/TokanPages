using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Controllers.Articles.Model;

namespace TokanPages.BackEnd.Logic.Articles 
{

    public interface IArticles 
    {
        Task<List<ArticleItem>> GetAllArticles();
        Task<ArticleItem> GetSingleArticle(string Id);
        Task<string> AddNewArticle(ArticleRequest PayLoad);
        Task UpdateArticle(ArticleRequest PayLoad);
        Task DeleteArticle(string Id);
    }

}
