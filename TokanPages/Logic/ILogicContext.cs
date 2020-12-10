using TokanPages.Logic.Mailer;
using TokanPages.Logic.Subscribers;
using TokanPages.Logic.Articles;
using TokanPages.Logic.MailChecker;

namespace TokanPages.Logic 
{

    public interface ILogicContext
    {
        public IArticles Articles { get; }
        public IMailer Mailer { get; }
        public IMailChecker MailChecker { get; }
        public ISubscribers Subscribers { get; }
    }

}
