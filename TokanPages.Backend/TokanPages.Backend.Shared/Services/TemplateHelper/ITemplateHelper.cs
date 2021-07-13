namespace TokanPages.Backend.Shared.Services.TemplateHelper
{
    using System.Collections.Generic;
    using Models;

    public interface ITemplateHelper
    {
        string MakeBody(string ATemplate, IEnumerable<TemplateItem> AItems);
    }
}