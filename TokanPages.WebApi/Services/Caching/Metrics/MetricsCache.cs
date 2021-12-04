namespace TokanPages.WebApi.Services.Caching.Metrics
{
    using System;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Core.Exceptions;
    using Backend.Shared.Services;
    using Backend.Shared.Resources;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;
    using Backend.Core.Utilities.CustomHttpClient.Authentication;
    using FluentValidation.Results;

    public class MetricsCache : IMetricsCache
    {
        private readonly IRedisDistributedCache _redisDistributedCache;

        private readonly ICustomHttpClient _customHttpClient;

        private readonly IApplicationSettings _applicationSettings;

        public MetricsCache(ICustomHttpClient customHttpClient, IRedisDistributedCache redisDistributedCache, 
            IApplicationSettings applicationSettings)
        {
            _customHttpClient = customHttpClient;
            _redisDistributedCache = redisDistributedCache;
            _applicationSettings = applicationSettings;
        }

        public async Task<IActionResult> GetMetrics(string project, string metric, bool noCache = false)
        {
            ValidateArguments(new Dictionary<string, string>
            {
                [nameof(project)] = project, 
                [nameof(metric)] = metric
            });

            var key = $"{project}/{metric}";
            var requestUrl = $"{_applicationSettings.SonarQube.Server}/api/project_badges/measure?project={project}&metric={metric}";
            if (noCache)
                return await ExecuteRequest(requestUrl);

            var value = await _redisDistributedCache.GetObjectAsync<FileContentResult>(key);
            if (value is not null) return value;

            value = await ExecuteRequest(requestUrl);
            await _redisDistributedCache.SetObjectAsync(key, value);

            return value;
        }

        public async Task<IActionResult> GetQualityGate(string project, bool noCache = false)
        {
            ValidateArguments(new Dictionary<string, string>
            {
                [nameof(project)] = project
            });
            
            var requestUrl = $"{_applicationSettings.SonarQube.Server}/api/project_badges/quality_gate?project={project}";
            if (noCache)
                return await ExecuteRequest(requestUrl);

            var value = await _redisDistributedCache.GetObjectAsync<FileContentResult>(project);
            if (value is not null) return value;

            value = await ExecuteRequest(requestUrl);
            await _redisDistributedCache.SetObjectAsync(project, value);

            return value;
        }

        private static void ValidateArguments(IDictionary<string, string> properties)
        {
            var result = new ValidationResult(new List<ValidationFailure>());
            foreach (var (key, value) in properties)
            {
                if (!string.IsNullOrEmpty(value)) 
                    continue;

                var failure = new ValidationFailure(key, ErrorCodes.INVALID_ARGUMENT)
                {
                    ErrorCode = nameof(ErrorCodes.INVALID_ARGUMENT)
                };

                result.Errors.Add(failure);
            }

            if (result.Errors.Any())
                throw new ValidationException(result, ErrorCodes.INVALID_ARGUMENT);
        }

        private async Task<FileContentResult> ExecuteRequest(string requestUrl)
        {
            var authentication = new BasicAuthentication
            {
                Login = _applicationSettings.SonarQube.Token, 
                Password = string.Empty
            };

            var configuration = new Configuration
            {
                Url = requestUrl, 
                Method = "GET", 
                Authentication = authentication
            };

            var results = await _customHttpClient.Execute(configuration);
            if (results.StatusCode == HttpStatusCode.OK)
                return new FileContentResult(results.Content, results.ContentType?.MediaType);

            var message = results.Content is null 
                ? ErrorCodes.ERROR_UNEXPECTED 
                : Encoding.Default.GetString(results.Content);

            throw new Exception(message);
        }
    }
}