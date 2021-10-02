namespace TokanPages.Backend.Cqrs.Handlers.Queries.Content
{
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Dto.Content;
    using Core.Utilities.JsonSerializer;

    public class GetContentQueryHandler : TemplateHandler<GetContentQuery, GetContentQueryResult>
    {
        private const string DEFAULT_LANGUAGE = "en";
        
        private readonly HttpClient FHttpClient;

        private readonly IJsonSerializer FJsonSerializer; 
        
        private readonly AzureStorage FAzureStorage;

        public GetContentQueryHandler(HttpClient AHttpClient, IJsonSerializer AJsonSerializer, AzureStorage AAzureStorage)
        {
            FHttpClient = AHttpClient;
            FJsonSerializer = AJsonSerializer;
            FAzureStorage = AAzureStorage;
        }

        public override async Task<GetContentQueryResult> Handle(GetContentQuery ARequest, CancellationToken ACancellationToken)
        {
            var LSelectedLanguage = string.IsNullOrEmpty(ARequest.Language) 
                ? DEFAULT_LANGUAGE
                : ARequest.Language;

            var LComponentRequestUrl = $"{FAzureStorage.BaseUrl}/content/{ARequest.Type}s/{ARequest.Name}.json";
            var LComponentContent = await GetJsonData(LComponentRequestUrl, ACancellationToken);
            var LJsonToken = FJsonSerializer.Parse(LComponentContent);
            var LToken = LJsonToken?.SelectToken(ARequest.Name);

            return new GetContentQueryResult
            {
                ContentType = ARequest.Type,
                ContentName = ARequest.Name,
                Content = GetObjectByLanguage(LToken, ARequest.Name, LSelectedLanguage)
            };
        }

        private static dynamic GetObjectByLanguage(JToken AToken, string AComponentName, string ASelectedLanguage)
        {
            return AComponentName switch
            {
                // Components
                "activateAccount" => MapObjects<ActivateAccountDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "articleFeatures" => MapObjects<ArticleFeaturesDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "contactForm" => MapObjects<ContactFormDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "cookiesPrompt" => MapObjects<CookiesPromptDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "featured" => MapObjects<FeaturedDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "features" => MapObjects<FeaturesDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "footer" => MapObjects<FooterDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "header" => MapObjects<HeaderDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "navigation" => MapObjects<NavigationDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "newsletter" => MapObjects<NewsletterDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "notFound" => MapObjects<NotFoundDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "resetPassword" => MapObjects<ResetPasswordDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "testimonials" => MapObjects<TestimonialsDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "unsubscribe" => MapObjects<UnsubscribeDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "updatePassword" => MapObjects<UpdatePasswordDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "updateSubscriber" => MapObjects<UpdateSubscriberDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "userSignin" => MapObjects<UserSigninDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "userSignout" => MapObjects<UserSignoutDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "userSignup" => MapObjects<UserSignupDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "wrongPagePrompt" => MapObjects<WrongPagePromptDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                // Documents
                "terms" => MapObjects<DocumentDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "policy" => MapObjects<DocumentDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                "myStory" => MapObjects<DocumentDto>(AToken).SingleOrDefault(AItem => AItem.Language == ASelectedLanguage),
                _ => null
            };
        }

        private async Task<string> GetJsonData(string AUrl, CancellationToken ACancellationToken)
        {
            var LResponseContent = await FHttpClient.GetAsync(AUrl, ACancellationToken);

            if (LResponseContent.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
                    
            return await LResponseContent.Content.ReadAsStringAsync(ACancellationToken);
        }

        private static IEnumerable<T> MapObjects<T>(JToken AComponent) where T : new()
        {
            return AComponent switch
            {
                JArray => AComponent.ToObject<IEnumerable<T>>(),
                _ => new List<T>()
            };
        }
    }
}