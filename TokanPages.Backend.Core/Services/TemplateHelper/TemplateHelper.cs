using System.Linq;
using System.Collections.Generic;
using TokanPages.Backend.Core.Services.TemplateHelper.Model;

namespace TokanPages.Backend.Core.Services.TemplateHelper
{
    public sealed class TemplateHelper : ITemplateHelper
    {
        public string MakeBody(string ATemplate, List<Item> AItems)
        {
            if (AItems == null || !AItems.Any()) return null;

            return AItems.Aggregate(ATemplate, (ACurrent, AItem) 
                => ACurrent.Replace(AItem.Tag, AItem.Value));
        }
    }
}
