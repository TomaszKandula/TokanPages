namespace TokanPages.Services.HttpClientService;

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

public interface IHttpClientService
{
    Task<Results> Execute(Configuration configuration, CancellationToken cancellationToken = default);

    string SetAuthentication(string login, string password);

    string SetAuthentication(string token);

    string GetFirstEmptyParameterName(IDictionary<string, string> parameterList);
}