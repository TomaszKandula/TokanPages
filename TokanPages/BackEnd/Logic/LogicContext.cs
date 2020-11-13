using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Logic.Mailer;
using TokanPages.BackEnd.Logic.Articles;
using TokanPages.BackEnd.Logic.MailChecker;

namespace TokanPages.BackEnd.Logic 
{

    public class LogicContext : ILogicContext 
    {

        private readonly ICosmosDbService FCosmosDbService;
        private readonly SendGridKeys FSendGridKeys;

        private IArticles    FArticles;
        private IMailer      FMailer;
        private IMailChecker FMailChecker;

        public LogicContext(ICosmosDbService ACosmosDbService, SendGridKeys ASendGridKeys) 
        {
            FCosmosDbService = ACosmosDbService;
            FSendGridKeys = ASendGridKeys;
        }

        public IArticles Articles 
        { 
            get { if (FArticles == null) FArticles = new Articles.Articles(FCosmosDbService); return FArticles; } 
        }

        public IMailer Mailer 
        {
            get { if (FMailer == null) FMailer = new Mailer.Mailer(FSendGridKeys); return FMailer; }
        }

        public IMailChecker MailChecker 
        {
            get { if (FMailChecker == null) FMailChecker = new MailChecker.MailChecker(); return FMailChecker; }
        }

    }

}
