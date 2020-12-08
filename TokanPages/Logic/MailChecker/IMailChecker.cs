using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Logic.MailChecker
{

    public interface IMailChecker
    {
        List<CheckerResult> IsAddressCorrect(List<string> AEmailAddress);
        Task<bool> IsDomainCorrect(string AEmailAddress);
    }

}
