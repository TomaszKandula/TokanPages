namespace TokanPages.Backend.Shared.Services.TemplateHelper
{
    using System.Collections.Generic;

    public interface ITemplateHelper
    {
        string MakeBody(string ATemplate, IEnumerable<TemplateItemModel> AItems);
    }
}