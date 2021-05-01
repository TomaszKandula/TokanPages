# My web page

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
| TokanPages.Backend.Core | Reusable core elements |
| TokanPages.Backend.Cqrs | Handlers, mappers and related services |
| TokanPages.Backend.Database | Database context |
| TokanPages.Backend.Domain | Domain entities |
| TokanPages.Backend.Shared | Shared models and resources |
| TokanPages.Backend.SmtpClient | SmtpClient service |
| TokanPages.Backend.Storage | Azure Storage service |

_Tests_

| Folder | Description |
|--------|-------------|
| Backend.TestData | Test helpers |
| Backend.UnitTests | Handlers and validators tests |
| Backend.IntegrationTests | Http client tests |

Integration tests focuses on testing HTTP responses, dependencies and theirs configuration. Unit tests covers all the logic used in the controllers. All dependencies are mocked/faked. For mocking [Moq](https://github.com/moq/moq4) and [MockQueryable.Moq](https://github.com/romantitov/MockQueryable) have been used.

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

Validator is registered within the middleware pipeline, and if it exists (not null), then we execute it and raise an exception if invalid, otherwise we proceed. Note: `ValidationException.cs` inherits from `BusinessException.cs` which inherits form System.Exception.

Such setup allow to have very thin controllers, example endpoint:

```csharp
[HttpGet]
public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool AIsPublished = true) 
    => await FMediator.Send(new GetAllArticlesQuery { IsPublished = AIsPublished });
```

When we call `GetAllArticles` endpoint, it sends command `GetAllArticlesQuery` with given parameters. The appropiate handler is `GetAllArticlesQueryHandler`:

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
