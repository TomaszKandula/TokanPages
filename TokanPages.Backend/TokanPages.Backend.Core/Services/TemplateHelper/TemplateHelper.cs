using System.Linq;
using System.Collections.Generic;

namespace TokanPages.Backend.Core.Services.TemplateHelper
{
    public sealed class TemplateHelper : ITemplateHelper
    {
        public string MakeBody(string ATemplate, IEnumerable<TemplateItemModel> AItems)
        {
            if (string.IsNullOrEmpty(ATemplate) || string.IsNullOrWhiteSpace(ATemplate))
                return null;
            
            if (AItems == null) 
                return null;
            
            var LTemplateItemModels = AItems.ToList();
            if (!LTemplateItemModels.Any()) 
                return null;

            return LTemplateItemModels.Aggregate(ATemplate, (ACurrent, AItem) 
                => ACurrent.Replace(AItem.Tag, AItem.Value));
        }
    }
}
