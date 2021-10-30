namespace TokanPages.Backend.Core.Utilities.CustomHttpClient
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Authentication;
    using Models;

    public class CustomHttpClient : ICustomHttpClient
    {
        private const string Header = "Authorization";

        private const string Basic = "Basic";

        private const string Bearer = "Bearer";

        private readonly HttpClient _httpClient;

        public CustomHttpClient(HttpClient httpClient) => _httpClient = httpClient;

        public virtual async Task<Results> Execute(Configuration configuration, CancellationToken cancellationToken = default)
        {
            VerifyConfigurationArgument(configuration);
            using var request = new HttpRequestMessage(new HttpMethod(configuration.Method), configuration.Url);

            if (configuration.StringContent != null)
                request.Content = configuration.StringContent;
            
            switch (configuration.Authentication)
            {
                case BasicAuthentication basicAuthentication:
                    var basic = SetAuthentication(basicAuthentication.Login, basicAuthentication.Password);
                    request.Headers.TryAddWithoutValidation(Header, basic); 
                    break;
                
                case BearerAuthentication bearerAuthentication:
                    var bearer = SetAuthentication(bearerAuthentication.Token);
                    request.Headers.TryAddWithoutValidation(Header, bearer); 
                    break;
            }

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var contentType = response.Content.Headers.ContentType;
            var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

            return new Results
            {
                StatusCode = response.StatusCode,
                ContentType = contentType,
                Content = content
            };
        }

        public virtual string SetAuthentication(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException($"Argument '{nameof(login)}' cannot be null or empty.");

            var base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}"));
            return $"{Basic} {base64}";
        }

        public virtual string SetAuthentication(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException($"Argument '{nameof(token)}' cannot be null or empty.");

            return $"{Bearer} {token}";
        }

        public virtual string GetFirstEmptyParameterName(IDictionary<string, string> parameterList)
        {
            var parameters = parameterList
                .Where(parameter => string.IsNullOrEmpty(parameter.Value))
                .ToList();

            return parameters.Any() 
                ? parameters.Select(parameter => parameter.Key).FirstOrDefault() 
                : string.Empty;
        }

        private static void VerifyConfigurationArgument(Configuration configuration)
        {
            if (string.IsNullOrEmpty(configuration.Method))
                throw new ArgumentException($"Argument '{nameof(configuration.Method)}' cannot be null or empty.");

            if (string.IsNullOrEmpty(configuration.Url))
                throw new ArgumentException($"Argument '{nameof(configuration.Url)}' cannot be null or empty.");
        }
    }
}