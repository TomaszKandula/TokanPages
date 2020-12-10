using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Logic.MailChecker
{

    public interface IMailChecker
    {
        List<CheckerResult> IsAddressCorrect(List<string> AEmailAddress);
        Task<bool> IsDomainCorrect(string AEmailAddress);
    }

}
