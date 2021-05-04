# TokanPages

TokanPages is the repository that holds my web page to share my programming interests (among others), primarily Microsoft technologies. I also plan album functionality and commenting under articles among article editor and user account capabilities.

## Tech-Stack

### Front-end

1. React/Redux with TypeScript.
1. Material-UI.
1. Jest.
1. Axios.
1. AOS.
1. Validate.js.
1. Syntax Highlighter.
1. Emoji Render.
1. HTML Parser.
1. Husky.

The client app uses functional approach and React Hooks.

Tests are provided, but there is no full coverage yet.

### Back-end

1. WebAPI (NET Core 3.1 / C#).
1. Azure SQL Database (ORM in use: EF Core).
1. Azure Blob Storage.
1. MediatR library.
1. CQRS pattern with no event sourcing.
1. MailKit.
1. DnsClient.
1. FluentValidation.
1. Sentry.
1. SeriLog.
1. Swagger-UI.

Unit Tests and Integration Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

Project is dockerized and deployed via GitHub Actions to Azure App Service that uses Container Registry.

## Project structure

_TokanPages_

| Folder | Description |
|--------|-------------|
| ClientApp | Frontend in React |
| Configuration | Application dependencies |
| Controllers | WebAPI |
| Middleware | Custom middleware |

_Backend_

| Folder | Description |
|--------|-------------|
| Core | Reusable core elements |
| Cqrs | Handlers, mappers and related services |
| Database | Database context |
| Domain | Domain entities |
| Shared | Shared models and resources |
| SmtpClient | SmtpClient service |
| Storage | Azure Storage service |

_Tests_

| Folder | Description |
|--------|-------------|
| DataProviders | Test helpers |
| UnitTests | Handlers and validators tests |
| IntegrationTests | Http client tests |

Integration tests focuses on testing HTTP responses, dependencies and theirs configuration. Unit tests covers handlers and validators. All dependencies are mocked/faked. For mocking [Moq](https://github.com/moq/moq4) and [MockQueryable.Moq](https://github.com/romantitov/MockQueryable) have been used.

## CQRS

The project uses a CQRS architectural pattern with no event sourcing (changes to the application state are **not** stored as a sequence of events). I used the MediatR library (mediator pattern) with the handler template.

The file `TemplateHandler.cs` presented below allow easy registration (mapping the handlers).

```csharp
public abstract class TemplateHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected TemplateHandler() { }

    public abstract Task<TResult> Handle(TRequest ARequest, CancellationToken ACancellationToken);
}
```

To configure it, in `Dependencies.cs` (registered at startup), we invoke:

```csharp
private static void SetupMediatR(IServiceCollection AServices) 
{
    AServices.AddMediatR(AOption => AOption.AsScoped(), 
        typeof(TemplateHandler<IRequest, Unit>).GetTypeInfo().Assembly);

    AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    AServices.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
}
```

The two additional lines register both `LoggingBehaviour` and `FluentValidationBehavior` as scoped services. Thus we not only log event before and after handler execution, but also we perform validation of payload before executing the handler.

`LoggingBehaviour.cs`:

```csharp
public async Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
{
    FLogger.LogInfo($"Begin: Handle {typeof(TRequest).Name}");
    var LResponse = await ANext();
    FLogger.LogInfo($"Finish: Handle {typeof(TResponse).Name}");
    return LResponse;
}
```

Logging is part of the middleware pipeline, and as said, we log info before and after handler execution.

`FluentValidationBehavior.cs`:

```csharp
public Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
{
    if (FValidator == null) return ANext();

    var LValidationContext = new ValidationContext<TRequest>(ARequest);
    var LValidationResults = FValidator.Validate(LValidationContext);

    if (!LValidationResults.IsValid)
        throw new ValidationException(LValidationResults);

    return ANext();
}
```

Validator is registered within the middleware pipeline, and if it exists (not null), then we execute it and raise an exception if invalid, otherwise we proceed. Note: `ValidationException.cs` inherits from `BusinessException.cs` which inherits form `System.Exception`.

Such setup allow to have very thin controllers, example endpoint:

```csharp
[HttpGet]
public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool AIsPublished = true) 
    => await FMediator.Send(new GetAllArticlesQuery { IsPublished = AIsPublished });
```

When we call `GetAllArticles` endpoint, it sends `GetAllArticlesQuery` request with given parameters. The appropriate handler is `GetAllArticlesQueryHandler`:

```csharp
public class GetAllArticlesQueryHandler : TemplateHandler<GetAllArticlesQuery, IEnumerable<GetAllArticlesQueryResult>>
{
    private readonly DatabaseContext FDatabaseContext;

    public GetAllArticlesQueryHandler(DatabaseContext ADatabaseContext) 
        => FDatabaseContext = ADatabaseContext;

    public override async Task<IEnumerable<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery ARequest, CancellationToken ACancellationToken) 
    {
        var LArticles = await FDatabaseContext.Articles
            .AsNoTracking()
            .Where(AArticles => AArticles.IsPublished == ARequest.IsPublished)
            .Select(AFields => new GetAllArticlesQueryResult 
            { 
                Id = AFields.Id,
                Title = AFields.Title,
                Description = AFields.Description,
                IsPublished = AFields.IsPublished,
                ReadCount = AFields.ReadCount,
                CreatedAt = AFields.CreatedAt,
                UpdatedAt = AFields.UpdatedAt
            })
            .OrderByDescending(AArticles => AArticles.CreatedAt)
            .ToListAsync(ACancellationToken);

        return LArticles; 
    }
}
```

## Exception Handler

After adding custom exception handler to the middleware pipeline:

```csharp
AApplication.UseExceptionHandler(ExceptionHandler.Handle);
```

It will catch exceptions and sets HTTP status: bad request (400) or internal server error (500). Thus, if we throw an error (business or validation) manually in the handler, the response is appropriately set up.

```csharp
public static class ExceptionHandler
{
    public static void Handle(IApplicationBuilder AApplication)
    {
        AApplication.Run(async AHttpContext => 
        {
            var LExceptionHandlerPathFeature = AHttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var LErrorException = LExceptionHandlerPathFeature.Error;
            AHttpContext.Response.ContentType = "application/json";

            string LResult;
            switch (LErrorException)
            {
                case ValidationException LException:
                {
                    var LAppError = new ApplicationError(LException.ErrorCode, LException.Message, LException.ValidationResult);
                    LResult = JsonConvert.SerializeObject(LAppError);
                    AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                }

                case BusinessException LException:
                {
                    var LAppError = new ApplicationError(LException.ErrorCode, LException.Message);
                    LResult = JsonConvert.SerializeObject(LAppError);
                    AHttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                }

                default:
                {
                    var LAppError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
                    LResult = JsonConvert.SerializeObject(LAppError);
                    AHttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                }
            }

            CorsHeaders.Ensure(AHttpContext);
            await AHttpContext.Response.WriteAsync(LResult);
        });
    }
}
```

Please note that handlers usually contains manual business exceptions while having validation exceptions very rarely as they are typically raised by the `FluentValidation` before handler is invoked, an example being:

```csharp
public class RemoveArticleCommandHandler : TemplateHandler<RemoveArticleCommand, Unit>
{
    private readonly DatabaseContext FDatabaseContext;

    public RemoveArticleCommandHandler(DatabaseContext ADatabaseContext) 
        => FDatabaseContext = ADatabaseContext;

    public override async Task<Unit> Handle(RemoveArticleCommand ARequest, CancellationToken ACancellationToken) 
    {
        var LCurrentArticle = await FDatabaseContext.Articles
            .Where(AArticles => AArticles.Id == ARequest.Id)
            .ToListAsync(ACancellationToken);

        if (!LCurrentArticle.Any())
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        FDatabaseContext.Articles.Remove(LCurrentArticle.Single());
        await FDatabaseContext.SaveChangesAsync(ACancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}
```

These business exceptions (`ARTICLE_DOES_NOT_EXISTS`) shall never be an validation error (invoked by `FluentValidation`). Furthermore, it is unlikely that we would want to perform database requests during validation. The validator is responsible for ensuring that input data is valid (not for checking available articles etc.).

## Setting-up the database

Copy below code from appsettings.Development.json to your **user secrets**:

```
{
  "ConnectionStrings": 
  {
    "DbConnect": "set_env"
  }
}
```

### Development environment:

If `set_env` remains unchanged, the application will use SQL database in-memory. However, suppose one is willing to use a local/remote SQL database. In that case, they should replace `set_env` with a valid connection string (note: application always uses in-memory database when the connection string is invalid). Application seeds test when the existing tables are not populated, and migration is performed only on the local/remote SQL database.

### Manual migration

Go to Package Manager Console (PMC) to execute following command:

`Update-Database -StartupProject TokanPages -Project TokanPages.Backend.Database -Context DatabaseContext`

EF Core will create all the necessary tables. More on migrations here: [TokanPages.Backend.Database](https://github.com/TomaszKandula/TokanPages/tree/dev/Backend/TokanPages.Backend.Database).

## CI/CD

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build_test.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test.yml) - it builds .NET Core application and React application in Docker, then runs all the available tests (Frontend and Backend). Each PR will invoke this action.
1. [dev_build_test_push.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test_push.yml) - it builds and tests both backend and frontend along with an Docker image so it can be later manually uploaded. 
1. [master_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build_test_publish.yml) - it builds, tests and publish Docker image to the Azure WebApp.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
