namespace TokanPages.Backend.Shared.Services.TemplateService
{
    using System.Linq;
    using System.Collections.Generic;
    using Models;

    public sealed class TemplateService : ITemplateService
    {
        /// <summary>
        /// Converts text with tags to text with values.
        /// </summary>
        /// <param name="ATemplate">String with tags to be replaced by given values.</param>
        /// <param name="AItems">Collection of tags and values for replacement.</param>
        /// <returns>String with replaced tags by given values.</returns>
        public string MakeBody(string ATemplate, IEnumerable<TemplateItem> AItems)
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