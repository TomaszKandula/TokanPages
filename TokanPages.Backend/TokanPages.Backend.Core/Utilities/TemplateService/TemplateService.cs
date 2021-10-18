namespace TokanPages.Backend.Core.Utilities.TemplateService
{
    using System.Linq;
    using System.Collections.Generic;

    public sealed class TemplateService : ITemplateService
    {
        /// <summary>
        /// Converts text with tags to text with values.
        /// </summary>
        /// <param name="template">String with tags to be replaced by given values.</param>
        /// <param name="items">Collection of tags and values for replacement.</param>
        /// <returns>String with replaced tags by given values.</returns>
        public string MakeBody(string template, IDictionary<string, string> items)
        {
            if (string.IsNullOrEmpty(template) || string.IsNullOrWhiteSpace(template))
                return null;
            
            if (items == null) 
                return null;
            
            var templateItemModels = items.ToList();
            if (!templateItemModels.Any()) 
                return null;

            return templateItemModels.Aggregate(template, (current, item) 
                => current.Replace(item.Key, item.Value));
        }
    }
}