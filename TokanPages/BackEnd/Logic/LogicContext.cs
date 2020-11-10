using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Logic.Articles;

namespace TokanPages.BackEnd.Logic 
{

    public class LogicContext : ILogicContext 
    {

        private readonly ICosmosDbService FCosmosDbService;
        private IArticles FArticles;

        public LogicContext(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
        }

        public IArticles Articles 
        { 
            get { if (FArticles == null) FArticles = new Articles.Articles(FCosmosDbService); return FArticles; } 
        }

    }

}
