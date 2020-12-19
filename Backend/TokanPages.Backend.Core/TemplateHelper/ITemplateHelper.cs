using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Core.TemplateHelper.Model;

namespace TokanPages.Backend.Core.TemplateHelper
{

    public interface ITemplateHelper
    {
        Task<string> MakeBody(string ATemplate, List<Item> AValueTag, string ATemplateSource);
    }

}
