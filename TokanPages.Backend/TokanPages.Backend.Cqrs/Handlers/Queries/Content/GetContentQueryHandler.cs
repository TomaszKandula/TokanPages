namespace TokanPages.Backend.Cqrs.Handlers.Queries.Content
{
    using System.Net;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Dto.Content;
    using Core.Utilities.JsonSerializer;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class GetContentQueryHandler : TemplateHandler<GetContentQuery, GetContentQueryResult>
    {
        private const string DEFAULT_LANGUAGE = "en";
        
        private readonly ICustomHttpClient FCustomHttpClient;

        private readonly IJsonSerializer FJsonSerializer; 
        
        private readonly AzureStorage FAzureStorage;

        public GetContentQueryHandler(ICustomHttpClient ACustomHttpClient, IJsonSerializer AJsonSerializer, AzureStorage AAzureStorage)
        {
            FCustomHttpClient = ACustomHttpClient;
            FJsonSerializer = AJsonSerializer;
            FAzureStorage = AAzureStorage;
        }

        public override async Task<GetContentQueryResult> Handle(GetContentQuery ARequest, CancellationToken ACancellationToken)
        {
            var LSelectedLanguage = string.IsNullOrEmpty(ARequest.Language) 
                ? DEFAULT_LANGUAGE
                : ARequest.Language;

            if (ARequest.Type is not ("component" or "document"))
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED), ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED);

            var LComponentRequestUrl = $"{FAzureStorage.BaseUrl}/content/{ARequest.Type}s/{ARequest.Name}.json";
            var LComponentContent = await GetJsonData(LComponentRequestUrl, ACancellationToken);

            if (string.IsNullOrEmpty(LComponentContent))
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_EMPTY), ErrorCodes.COMPONENT_CONTENT_EMPTY);

            var LJsonToken = FJsonSerializer.Parse(LComponentContent);
            var LToken = LJsonToken?.SelectToken(ARequest.Name);

            if (LToken == null)
                throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN), ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN);

            return new GetContentQueryResult
            {
                ContentType = ARequest.Type,
                ContentName = ARequest.Name,
                Content = GetObjectByLanguage(LToken, ARequest.Name, LSelectedLanguage)
            };
        }

        private dynamic GetObjectByLanguage(JToken AToken, string AComponentName, string ASelectedLanguage)
        {
            return AComponentName switch
            {
                // Components
                "activateAccount" => FJsonSerializer.MapObjects<ActivateAccountDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "articleFeatures" => FJsonSerializer.MapObjects<ArticleFeaturesDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "contactForm" => FJsonSerializer.MapObjects<ContactFormDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "cookiesPrompt" => FJsonSerializer.MapObjects<CookiesPromptDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "featured" => FJsonSerializer.MapObjects<FeaturedDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "features" => FJsonSerializer.MapObjects<FeaturesDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "footer" => FJsonSerializer.MapObjects<FooterDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "header" => FJsonSerializer.MapObjects<HeaderDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "navigation" => FJsonSerializer.MapObjects<NavigationDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "newsletter" => FJsonSerializer.MapObjects<NewsletterDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "notFound" => FJsonSerializer.MapObjects<NotFoundDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "resetPassword" => FJsonSerializer.MapObjects<ResetPasswordDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "testimonials" => FJsonSerializer.MapObjects<TestimonialsDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "unsubscribe" => FJsonSerializer.MapObjects<UnsubscribeDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "updatePassword" => FJsonSerializer.MapObjects<UpdatePasswordDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "updateSubscriber" => FJsonSerializer.MapObjects<UpdateSubscriberDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "userSignin" => FJsonSerializer.MapObjects<UserSigninDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "userSignout" => FJsonSerializer.MapObjects<UserSignoutDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "userSignup" => FJsonSerializer.MapObjects<UserSignupDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "wrongPagePrompt" => FJsonSerializer.MapObjects<WrongPagePromptDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                // Documents
                "terms" => FJsonSerializer.MapObjects<DocumentDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "policy" => FJsonSerializer.MapObjects<DocumentDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "myStory" => FJsonSerializer.MapObjects<DocumentDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                _ => throw new BusinessException(nameof(ErrorCodes.COMPONENT_NAME_UNKNOWN), ErrorCodes.COMPONENT_NAME_UNKNOWN)
            };
        }

        private async Task<string> GetJsonData(string AUrl, CancellationToken ACancellationToken)
        {
            var LConfiguration = new Configuration { Url = AUrl, Method = "GET" };
            var LResults = await FCustomHttpClient.Execute(LConfiguration, ACancellationToken);

            if (LResults.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

            return LResults.Content == null 
                ? string.Empty 
                : System.Text.Encoding.Default.GetString(LResults.Content);
        }
    }
}