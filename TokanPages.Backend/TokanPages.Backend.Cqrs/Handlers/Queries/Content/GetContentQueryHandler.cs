namespace TokanPages.Backend.Cqrs.Handlers.Queries.Content;

using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Persistence.Database;
using Core.Exceptions;
using Shared.Resources;
using Dto.Content;
using Core.Utilities.LoggerService;
using Core.Utilities.JsonSerializer;
using Services.AzureStorageService.Factory;

public class GetContentQueryHandler : RequestHandler<GetContentQuery, GetContentQueryResult>
{
    private const string DefaultLanguage = "eng";
        
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IJsonSerializer _jsonSerializer;

    public GetContentQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IJsonSerializer jsonSerializer, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _jsonSerializer = jsonSerializer;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<GetContentQueryResult> Handle(GetContentQuery request, CancellationToken cancellationToken)
    {
        var selectedLanguage = string.IsNullOrEmpty(request.Language) 
            ? DefaultLanguage
            : request.Language;

        if (request.Type is not ("component" or "document"))
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED), ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED);
        
        var componentRequestUrl = $"content/{request.Type}s/{request.Name}.json";
        var azureBob = _azureBlobStorageFactory.Create();
        var contentStream = await azureBob.OpenRead(componentRequestUrl, cancellationToken);

        if (contentStream is null)
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_NOT_FOUND), ErrorCodes.COMPONENT_NOT_FOUND);

        if (contentStream.Content is null)
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_NOT_FOUND), ErrorCodes.COMPONENT_NOT_FOUND);

        var memoryStream = new MemoryStream();
        await contentStream.Content.CopyToAsync(memoryStream, cancellationToken);
        var componentContent = Encoding.Default.GetString(memoryStream.ToArray());

        if (string.IsNullOrEmpty(componentContent))
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_EMPTY), ErrorCodes.COMPONENT_CONTENT_EMPTY);

        var jsonToken = _jsonSerializer.Parse(componentContent);
        var token = jsonToken.SelectToken(request.Name);

        if (token == null)
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN), ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN);

        return new GetContentQueryResult
        {
            ContentType = request.Type,
            ContentName = request.Name,
            Content = GetObjectByLanguage(token, request.Name, selectedLanguage)
        };
    }

    private dynamic? GetObjectByLanguage(JToken token, string componentName, string selectedLanguage)
    {
        return componentName switch
        {
            // Components
            "account" => _jsonSerializer.MapObjects<AccountDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
            "activateAccount" => _jsonSerializer.MapObjects<ActivateAccountDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
            "articleFeatures" => _jsonSerializer.MapObjects<ArticleFeaturesDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
            "contactForm" => _jsonSerializer.MapObjects<ContactFormDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
            "cookiesPrompt" => _jsonSerializer.MapObjects<CookiesPromptDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
            "clients" => _jsonSerializer.MapObjects<ClientsContentDto>(token).SingleOrDefault(item => item.Language == selectedLanguage),
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
}