namespace TokanPages.Backend.Shared.Services.TemplateService
{
    using System.Collections.Generic;
    using Models;

    public interface ITemplateService
    {
        string MakeBody(string ATemplate, IEnumerable<TemplateItem> AItems);
    }
}