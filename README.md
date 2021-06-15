## TokanPages
### Microsoft technologies and more

TokanPages is the repository that holds my web page to share my programming interests (among others), primarily Microsoft technologies. I also plan album functionality and comment section under articles among article editor and user account capabilities.

## Project metrics
### Client-App

<p>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=ncloc&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=code_smells&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=security_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=sqale_index&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-frontend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-frontend&ametric=duplicated_lines_density&kill_cache=1">
  </a>
</p>

### WebApi / Backend

<p>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=ncloc&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=code_smells&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=security_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=sqale_index&kill_cache=1">
  </a>
  <a href="https://tokansonar.azurewebsites.net/dashboard?id=tokanpages-backend">
    <img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/sonarqube/metrics?aproject=tokanpages-backend&ametric=duplicated_lines_density&kill_cache=1">
  </a>
</p>

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
1. Semantic-Release.

The client app uses functional approach and React Hooks.

Tests are provided, but there is no full coverage yet.

<img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/SonarQube/Metrics/Quality?AProject=tokanpages-frontend&kill_cache=1">

### Back-end

1. WebAPI (NET 5, C#).
1. Azure SQL Database (with EF Core).
1. Azure Blob Storage.
1. MediatR library.
1. CQRS pattern with no event sourcing.
1. MailKit.
1. DnsClient.
1. FluentValidation.
1. Sentry.
1. SeriLog.
1. Swagger-UI.
1. Polly.

Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

Project is dockerized and deployed via GitHub Actions to Azure App Service that uses Container Registry.

<img alt="" src="https://tokanpages-backend-staging.azurewebsites.net/api/v1/SonarQube/Metrics/Quality?AProject=tokanpages-backend&kill_cache=1">

## Project structure

_TokanPages.ClientApp_

| Folder | Description |
|--------|-------------|
| nginx | WebServer configuration |
| public | WebApp entrypoint |
| src | Frontend in React |

React application runs on NGINX in Docker. It is deployed on main domain.

Unit tests for the frontend are provided; use command `yarn test` to run all tests.

_TokanPages.Backend_

| Folder | Description |
|--------|-------------|
| Backend.Core | Reusable core elements |
| Backend.Cqrs | Handlers, mappers and related services |
| Backend.Database | Database context |
| Backend.Domain | Domain entities |
| Backend.Shared | Shared models and resources |
| Backend.SmtpClient | SmtpClient service |
| Backend.Storage | Azure Storage service |

_TokanPages.Backend.Tests_

| Folder | Description |
|--------|-------------|
| Backend.Tests | Handlers and validators tests |

Tests covers handlers and validators. All dependencies are mocked. For mocking [Moq](https://github.com/moq/moq4) has been used.

_TokanPages.WebApi_

| Folder | Description |
|--------|-------------|
| Configuration | Application dependencies |
| Controllers | WebApi |
| Middleware | Custom middleware |
| Properties | Lunch settings |

_TokanPages.WebApi.Tests_

| Folder | Description |
|--------|-------------|
| WebApi.Tests | Http client tests |

Tests focuses on testing HTTP client responses, dependencies and theirs configuration. 

To run backend tests, use command `dotnet test`.

## Testing

### Backend tests setup (unit tests)

Unit tests use SQLite in-memory database (a lightweight database that supports RDBMS). Each test uses a separate database instance, and therefore tables must be populated before a test can be run. Database instances are provided via the factory:

```csharp
internal class DatabaseContextFactory
{
    private readonly DbContextOptionsBuilder<DatabaseContext> FDatabaseOptions =
        new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite("Data Source=InMemoryDatabase;Mode=Memory");

    public DatabaseContext CreateDatabaseContext()
    {
        var LDatabaseContext = new DatabaseContext(FDatabaseOptions.Options);
        LDatabaseContext.Database.OpenConnection();
        LDatabaseContext.Database.EnsureCreated();
        return LDatabaseContext;
    }
}
```

Each test can easily access `CreateDatabaseContext()` method via `GetTestDatabaseContext()` as long as test class inherits from `TestBase` class:

```csharp
public class TestBase
{
    private readonly DatabaseContextFactory FDatabaseContextFactory;

    protected TestBase() 
    {
        var LServices = new ServiceCollection();

        LServices.AddSingleton<DatabaseContextFactory>();
        LServices.AddScoped(AContext =>
        {
            var LFactory = AContext.GetService<DatabaseContextFactory>();
            return LFactory?.CreateDatabaseContext();
        });

        var LServiceScope = LServices.BuildServiceProvider(true).CreateScope();
        var LServiceProvider = LServiceScope.ServiceProvider;
        FDatabaseContextFactory = LServiceProvider.GetService<DatabaseContextFactory>();
    }

    protected DatabaseContext GetTestDatabaseContext()
        =>  FDatabaseContextFactory.CreateDatabaseContext();
}
```

### WebApi tests setup (integration tests)

Integration test uses SQL Server database either local or remote, accordingly to a given connection string. Each test class uses `WebApplicationFactory`:

```csharp
public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
{
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        var LBuilder = WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(AConfig =>
            {
                var LStartupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                var LTestConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets(LStartupAssembly)
                    .AddEnvironmentVariables()
                    .Build();
              
                AConfig.AddConfiguration(LTestConfig);
            })
            .UseStartup<TTestStartup>()
            .UseTestServer();
            
        return LBuilder;
    }
}
```

I use `user secrets` with a connection string for local development, pointing to an instance of SQL Express that runs in Docker. However, when the test project runs in CI/CD pipeline, it uses a connection string defined in `appsettings.Staging.json` and connects with a remote test database.

Class `CustomWebApplicationFactory` requires the `Startup` class to configure necessary services. Thus test project has its own `TestStartup.cs`. We register only essential services.

Note: before integration tests can run, the test database must be already up.

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
public class CustomException
{
    private readonly RequestDelegate FRequestDelegate;
        
    public CustomException(RequestDelegate ARequestDelegate) 
        => FRequestDelegate = ARequestDelegate;
        
    public async Task Invoke(HttpContext AHttpContext)
    {
        try
        {
            await FRequestDelegate.Invoke(AHttpContext);
        }
        catch (ValidationException LValidationException)
        {
            var LApplicationError = new ApplicationError(LValidationException.ErrorCode, LValidationException.Message, LValidationException.ValidationResult);
            await WriteErrorResponse(AHttpContext, LApplicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
        }
        catch (BusinessException LBusinessException)
        {
            var LApplicationError = new ApplicationError(LBusinessException.ErrorCode, LBusinessException.Message);
            await WriteErrorResponse(AHttpContext, LApplicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
        }
        catch (Exception LException)
        {
            var LApplicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED, LException.Message);
            await WriteErrorResponse(AHttpContext, LApplicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
        }
    }

    private static Task WriteErrorResponse(HttpContext AHttpContext, ApplicationError AApplicationError, HttpStatusCode AStatusCode)
    {
        var LResult = JsonSerializer.Serialize(AApplicationError);
        AHttpContext.Response.ContentType = "application/json";
        AHttpContext.Response.StatusCode = (int)AStatusCode;
        CorsHeaders.Ensure(AHttpContext);
        return AHttpContext.Response.WriteAsync(LResult);
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
    "DbConnect": "set_env",
    "DbConnectTest": "set_env"
  }
}
```

### Development environment:

Replace `set_env` with connection strings of choice. Please note that `DbConnect` points to a main database (local development / production), and `DbConnectTest` points to a test database for integration tests only. Application migarte and seed tests data when run in development mode, however, for integration tests, test database must be already up.

### Database migration

Go to Package Manager Console (PMC) to execute following command:

`Update-Database -StartupProject TokanPages -Project TokanPages.Backend.Database -Context DatabaseContext`

EF Core will create all the necessary tables and seed test data. More on migrations here: [TokanPages.Backend.Database](https://github.com/TomaszKandula/TokanPages/tree/dev/Backend/TokanPages.Backend.Database).

## CI/CD

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build_test.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test.yml) - it builds .NET Core application and React application, then runs all the available tests; finally, it scans the code with SonarQube.
1. [stage_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/stage_build_test_publish.yml) - it builds, tests and publishes Docker image to the staging Azure WebApp.
1. [master_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build_test_publish.yml) - it builds, tests and publishes Docker image to the production Azure WebApp.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
