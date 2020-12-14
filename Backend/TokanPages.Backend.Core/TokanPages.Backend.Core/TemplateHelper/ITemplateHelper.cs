using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Core.Models;

namespace TokanPages.Backend.Core.TemplateHelper
{

    public interface ITemplateHelper
    {
        Task<string> MakeBody(string ATemplate, List<ValueTag> AValueTag, string ATemplateSource);
    }

}
