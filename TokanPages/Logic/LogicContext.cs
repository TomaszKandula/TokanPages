using TokanPages.Logic.Mailer;
using TokanPages.Logic.Articles;
using TokanPages.Logic.Subscribers;
using TokanPages.Logic.MailChecker;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Database;
using TokanPages.Backend.SmtpClient;

namespace TokanPages.Logic 
{

    public class LogicContext : ILogicContext 
    {

        private readonly ICosmosDbService     FCosmosDbService;
        private readonly ISmtpClientService   FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;

        private IArticles    FArticles;
        private IMailer      FMailer;
        private IMailChecker FMailChecker;
        private ISubscribers FSubscribers;

        public LogicContext(ICosmosDbService ACosmosDbService, ISmtpClientService ASmtpClientService, IAzureStorageService AAzureStorageService) 
        {
            FCosmosDbService     = ACosmosDbService;
            FSmtpClientService   = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
        }

        public IArticles Articles 
        { 
            get { if (FArticles == null) FArticles = new Articles.Articles(FCosmosDbService); return FArticles; } 
        }

        public IMailer Mailer 
        {
            get { if (FMailer == null) FMailer = new Mailer.Mailer(FSmtpClientService, FAzureStorageService); return FMailer; }
        }

        public IMailChecker MailChecker 
        {
            get { if (FMailChecker == null) FMailChecker = new MailChecker.MailChecker(); return FMailChecker; }
        }

        public ISubscribers Subscribers
        {
            get { if (FSubscribers == null) FSubscribers = new Subscribers.Subscribers(FCosmosDbService); return FSubscribers; }
        }

    }

}
