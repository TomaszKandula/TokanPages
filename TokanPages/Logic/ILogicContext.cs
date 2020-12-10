using TokanPages.Logic.Mailer;
using TokanPages.Logic.Articles;
using TokanPages.Logic.MailChecker;
using TokanPages.Logic.Subscribers;

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
