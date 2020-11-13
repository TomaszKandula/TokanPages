using TokanPages.BackEnd.Logic.Mailer;
using TokanPages.BackEnd.Logic.Articles;
using TokanPages.BackEnd.Logic.MailChecker;

namespace TokanPages.BackEnd.Logic 
{

    public interface ILogicContext 
    {
        public IArticles Articles { get;  }
        public IMailer Mailer { get; }
        public IMailChecker MailChecker { get; }
    }

}
