using System.Linq;
using System.Collections.Generic;
using TokanPages.Backend.Core.TemplateHelper.Model;

namespace TokanPages.Backend.Core.TemplateHelper
{

    public class TemplateHelper : ITemplateHelper
    {

        public string MakeBody(string LTemplate, List<Item> AItems)
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
