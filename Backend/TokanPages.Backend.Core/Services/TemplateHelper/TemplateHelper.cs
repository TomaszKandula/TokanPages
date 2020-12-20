using System.Linq;
using System.Collections.Generic;
using TokanPages.Backend.Core.TemplateHelper.Model;

namespace TokanPages.Backend.Core.Services.TemplateHelper
{

    public class TemplateHelper : ITemplateHelper
    {

        public virtual string MakeBody(string LTemplate, List<Item> AItems)
        {

            if (AItems == null || !AItems.Any()) return null;

            foreach (var AItem in AItems)
            {
                LTemplate = LTemplate.Replace(AItem.Tag, AItem.Value);
            }

            return LTemplate;

        }

    }

}
