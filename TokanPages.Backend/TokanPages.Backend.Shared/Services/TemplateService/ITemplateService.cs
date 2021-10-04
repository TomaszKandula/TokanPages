namespace TokanPages.Backend.Shared.Services.TemplateService
{
    using System.Collections.Generic;

    public interface ITemplateService
    {
        string MakeBody(string ATemplate, IDictionary<string, string> AItems);
    }
}