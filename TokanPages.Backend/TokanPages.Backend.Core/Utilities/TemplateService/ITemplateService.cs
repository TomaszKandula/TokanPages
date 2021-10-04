namespace TokanPages.Backend.Core.Utilities.TemplateService
{
    using System.Collections.Generic;

    public interface ITemplateService
    {
        string MakeBody(string ATemplate, IDictionary<string, string> AItems);
    }
}