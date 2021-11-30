namespace TokanPages.Backend.Cqrs.Handlers.Queries.Content
{
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Database;
    using Core.Exceptions;
    using Shared.Services;
    using Shared.Resources;
    using Shared.Dto.Content;
    using Core.Utilities.LoggerService;
    using Core.Utilities.JsonSerializer;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class GetContentQueryHandler : TemplateHandler<GetContentQuery, GetContentQueryResult>
    {
        private const string DefaultLanguage = "en";
        
        private readonly ICustomHttpClient _customHttpClient;

        private readonly IJsonSerializer _jsonSerializer;

        private readonly IApplicationSettings _applicationSettings;

        public GetContentQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, ICustomHttpClient customHttpClient, 
            IJsonSerializer jsonSerializer, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
        {
            _customHttpClient = customHttpClient;
            _jsonSerializer = jsonSerializer;
            _applicationSettings = applicationSettings;
        }

        public override async Task<GetContentQueryResult> Handle(GetContentQuery request, CancellationToken cancellationToken)
        {
            var selectedLanguage = string.IsNullOrEmpty(request.Language) 
                ? DefaultLanguage
                : request.Language;

            if (request.Type is not ("component" or "document"))
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED), ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED);

            var componentRequestUrl = $"{_applicationSettings.AzureStorage.BaseUrl}/content/{request.Type}s/{request.Name}.json";
            var componentContent = await GetJsonData(componentRequestUrl, cancellationToken);

            if (string.IsNullOrEmpty(componentContent))
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_EMPTY), ErrorCodes.COMPONENT_CONTENT_EMPTY);

            var jsonToken = _jsonSerializer.Parse(componentContent);
            var token = jsonToken?.SelectToken(request.Name);

            if (token == null)
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN), ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN);

            return new GetContentQueryResult
            {
                ContentType = request.Type,
                ContentName = request.Name,
                Content = GetObjectByLanguage(token, request.Name, selectedLanguage)
            };
        }

        private dynamic GetObjectByLanguage(JToken token, string componentName, string selectedLanguage)
        {
            return componentName switch
            {
                // Components
                "activateAccount" => _jsonSerializer.MapObjects<ActivateAccountDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "articleFeatures" => _jsonSerializer.MapObjects<ArticleFeaturesDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "contactForm" => _jsonSerializer.MapObjects<ContactFormDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "cookiesPrompt" => _jsonSerializer.MapObjects<CookiesPromptDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "featured" => _jsonSerializer.MapObjects<FeaturedDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "features" => _jsonSerializer.MapObjects<FeaturesDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "footer" => _jsonSerializer.MapObjects<FooterDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "header" => _jsonSerializer.MapObjects<HeaderDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "navigation" => _jsonSerializer.MapObjects<NavigationDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "newsletter" => _jsonSerializer.MapObjects<NewsletterDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "notFound" => _jsonSerializer.MapObjects<NotFoundDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "resetPassword" => _jsonSerializer.MapObjects<ResetPasswordDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "testimonials" => _jsonSerializer.MapObjects<TestimonialsDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "unsubscribe" => _jsonSerializer.MapObjects<UnsubscribeDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "updatePassword" => _jsonSerializer.MapObjects<UpdatePasswordDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "updateSubscriber" => _jsonSerializer.MapObjects<UpdateSubscriberDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "userSignin" => _jsonSerializer.MapObjects<UserSigninDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "userSignout" => _jsonSerializer.MapObjects<UserSignoutDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "userSignup" => _jsonSerializer.MapObjects<UserSignupDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "wrongPagePrompt" => _jsonSerializer.MapObjects<WrongPagePromptDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                // Documents
                "terms" => _jsonSerializer.MapObjects<DocumentDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "policy" => _jsonSerializer.MapObjects<DocumentDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                "myStory" => _jsonSerializer.MapObjects<DocumentDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
                _ => throw new BusinessException(nameof(ErrorCodes.COMPONENT_NAME_UNKNOWN), ErrorCodes.COMPONENT_NAME_UNKNOWN)
            };
        }

        private async Task<string> GetJsonData(string url, CancellationToken cancellationToken)
        {
            var configuration = new Configuration { Url = url, Method = "GET" };
            var results = await _customHttpClient.Execute(configuration, cancellationToken);

            if (results.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_NOT_FOUND), ErrorCodes.COMPONENT_NOT_FOUND);

            return results.Content == null 
                ? string.Empty 
                : Encoding.Default.GetString(results.Content);
        }
    }
}