using System.Collections.Generic;
using TokanPages.BackEnd.Database.Model;

namespace BackEnd.UnitTests.Mock
{

    public static class DummyData
    {

        public static List<Article> ReturnDummyArticles() 
        {

            return new List<Article>
            {
                
                new Article
                { 
                    Id     = "80cc8b7b-56f6-4e9d-8e17-0dc010b892d2",
                    Title  = "ABC",
                    Desc   = "Lorem ipsum...",
                    Status = "draft",
                    Likes  = 0
                },
                new Article
                {
                    Id     = "e8722b93-5f99-4fec-996b-3a3bd401079f",
                    Title  = "DEF",
                    Desc   = "Lorem ipsum...",
                    Status = "draft",
                    Likes  = 0
                },
                new Article
                {
                    Id     = "66ab39af-424c-454d-a942-1c2977632fb8",
                    Title  = "QWERTY",
                    Desc   = "Lorem ipsum...",
                    Status = "published",
                    Likes  = 0
                },
                new Article
                {
                    Id     = "f29306f9-36fe-4935-8f86-fb448a21019c",
                    Title  = "ZXC",
                    Desc   = "Lorem ipsum...",
                    Status = "draft",
                    Likes  = 0
                },
                new Article
                {
                    Id     = "7b5c33fc-9ff0-4a8c-83b4-5c4301b60251",
                    Title  = "POI",
                    Desc   = "Lorem ipsum...",
                    Status = "draft",
                    Likes  = 0
                },
                new Article
                {
                    Id     = "4d9b0aad-7b69-4f12-a5cf-7308f33cffd0",
                    Title  = "MNB",
                    Desc   = "Lorem ipsum...",
                    Status = "published",
                    Likes  = 0
                },

            };
       
        }

    }

}
