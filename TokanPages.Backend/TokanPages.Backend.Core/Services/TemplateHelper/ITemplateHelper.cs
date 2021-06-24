using System.Collections.Generic;

namespace TokanPages.Backend.Core.Services.TemplateHelper
{
    public interface ITemplateHelper
    {
        string MakeBody(string ATemplate, IEnumerable<TemplateItemModel> AItems);
    }
}
