namespace TokanPages.Backend.Core.Utilities.TemplateService;

using System.Collections.Generic;

public interface ITemplateService
{
    string MakeBody(string template, IDictionary<string, string> items);
}