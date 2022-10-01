## TokanPages
### Microsoft technologies and more

TokanPages is the repository that holds my web page to share my programming interests (among others), primarily Microsoft technologies. I also plan album functionality and comment section under articles among article editor and user account capabilities.

## CI/CD Pipelines

[![Build & run tests (dev)](https://github.com/TomaszKandula/TokanPages/actions/workflows/dev_build_test.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/dev_build_test.yml)

[![Build, test and publish (stage)](https://github.com/TomaszKandula/TokanPages/actions/workflows/stage_build_test_publish.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/stage_build_test_publish.yml)

[![Build, test and publish (master)](https://github.com/TomaszKandula/TokanPages/actions/workflows/master_build_test_publish.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/master_build_test_publish.yml)

## Project metrics
### Client-App

<p>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=ncloc&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=code_smells&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=security_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=sqale_index&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=duplicated_lines_density&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-frontend&metric=coverage&kill_cache=1">
  </a>
</p>

### WebApi / Backend

<p>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=ncloc&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=code_smells&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=security_rating&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=sqale_index&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=duplicated_lines_density&kill_cache=1">
  </a>
  <a href="https://tokansonar-app.azurewebsites.net/about">
    <img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/metrics?project=tokanpages-backend&metric=coverage&kill_cache=1">
  </a>
</p>

## Tech-Stack

### Front-end

1. React/Redux with TypeScript.
1. Material-UI.
1. JEST.
1. Axios.
1. AOS.
1. Validate.js.
1. Syntax Highlighter.
1. Emoji Render.
1. HTML Parser.
1. Husky.
1. Semantic-Release.
1. NGINX.

The client app uses React Hooks. Tests are provided using JEST, but there is no full coverage yet.

Project is dockerized and deployed via GitHub Actions to Azure App Service (main domain) that uses Container Registry. Web Server of choice is NGINX.

<img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/Metrics/Quality?Project=tokanpages-frontend&kill_cache=1">

### Back-end

1. WebApi (NET 6, C#).
1. Azure SQL Database (with EF Core).
1. Azure Blob Storage.
1. Azure Redis Cache.
1. MediatR library.
1. CQRS pattern with no event sourcing.
1. FluentValidation.
1. SeriLog.
1. Swagger-UI.
1. Polly.

Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

Project is dockerized and deployed via GitHub Actions to Azure App Service that uses Container Registry.

<img alt="" src="https://tokansonar-proxy.azurewebsites.net/api/v1/Metrics/Quality?Project=tokanpages-backend&kill_cache=1">

## Project structure

_TokanPages.ClientApp_

| Folder | Description |
|--------|-------------|
| nginx | WebServer configuration |
| public | WebApp entrypoint |
| src | Frontend in React |

React application runs on NGINX in Docker. It is deployed on the main domain.

Unit tests for the frontend are provided; use command `yarn app-test` to run all tests.

_TokanPages.Backend_

| Folder | Description |
|--------|-------------|
| Backend.Core | Reusable core elements |
| Backend.Cqrs | Handlers, mappers and related services |
| Backend.Database | Database context |
| Backend.Domain | Domain entities, contracts and enums |
| Backend.Shared | Shared models and resources |
| Backend.Storage | Azure Storage service |

_TokanPages.Services_

| Folder | Description |
|--------|-------------|
| Services.AzureStorageService | Application remote storage |
| Services.BehaviourService | MediatR pipelines |
| Services.CipheringService | Password hashing |
| Services.EmailSenderService | Email handling |
| Services.HttpClientService | Custom HTTP client |
| Services.UserService | User provider |
| Services.WebTokenService | JWT handling |

_TokanPages.WebApi_

| Folder | Description |
|--------|-------------|
| Properties | Lunch settings |
| Configuration | Application dependencies |
| Controllers | WebApi |
| Middleware | Custom middleware |
| Services | WebApi Services |

WebApi services currently holds only data caching (Azure Redis Cache) for all query actions (http GET method).

_TokanPages.Tests_

| Folder | Description |
|--------|-------------|
| IntegrationTests | Http client tests |
| UnitTests | Handlers and validators tests |

Unit tests covers handlers and validators. All dependencies are mocked. For mocking [Moq](https://github.com/moq/moq4) has been used.

Integration tests focuses on testing HTTP client responses, dependencies and theirs configuration.

To run backend tests, use command `dotnet test`.

## Testing

### Backend/Services tests setup (unit tests)

Unit tests use SQLite in-memory database (a lightweight database that supports RDBMS). Each test uses a separate database instance, and therefore tables must be populated before a test can be run. Database instances are provided via the factory:

```csharp
internal class DatabaseContextFactory
{
    private readonly DbContextOptionsBuilder<DatabaseContext> _databaseOptions =
        new DbContextOptionsBuilder<DatabaseContext>()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging()
            .UseSqlite("Data Source=InMemoryDatabase;Mode=Memory");

    public DatabaseContext CreateDatabaseContext()
    {
        var databaseContext = new DatabaseContext(_databaseOptions.Options);
        databaseContext.Database.OpenConnection();
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }
}
```

Each test can easily access `CreateDatabaseContext()` method via `GetTestDatabaseContext()` as long as test class inherits from `TestBase` class:

```csharp
public class TestBase
{
    protected IDataUtilityService DataUtilityService { get; }
    protected IJwtUtilityService JwtUtilityService { get; }
    protected IDateTimeService DateTimeService { get; }
    private readonly DatabaseContextFactory _databaseContextFactory;
        
    protected TestBase()
    {
        DataUtilityService = new DataUtilityService();
        JwtUtilityService = new JwtUtilityService();
        DateTimeService = new DateTimeService();

        var services = new ServiceCollection();
        services.AddSingleton<DatabaseContextFactory>();
        services.AddScoped(context =>
        {
            var factory = context.GetService<DatabaseContextFactory>();
            return factory?.CreateDatabaseContext();
        });

        var serviceScope = services.BuildServiceProvider(true).CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        _databaseContextFactory = serviceProvider.GetService<DatabaseContextFactory>();
    }

    protected DatabaseContext GetTestDatabaseContext() 
        => _databaseContextFactory.CreateDatabaseContext();
}
```

### WebApi tests setup (integration tests)

Integration test uses SQL Server database either local or remote, accordingly to a given connection string. Each test class uses `WebApplicationFactory`:

```csharp
public class CustomWebApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
{
    public string WebSecret { get; private set; }
    public string Issuer { get; private set; }
    public string Audience { get; private set; }
    public string Connection { get; private set; }
        
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        var builder = WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(configurationBuilder =>
            {
                var startupAssembly = typeof(TTestStartup).GetTypeInfo().Assembly;
                var testConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets(startupAssembly, true)
                    .AddEnvironmentVariables()
                    .Build();
                
                configurationBuilder.AddConfiguration(testConfig);

                var config = configurationBuilder.Build();
                Issuer = config.GetValue<string>("IdentityServer:Issuer");
                Audience = config.GetValue<string>("IdentityServer:Audience");
                WebSecret = config.GetValue<string>("IdentityServer:WebSecret");
                Connection = config.GetValue<string>("ConnectionStrings:DbConnectTest");
            })
            .UseStartup<TTestStartup>()
            .UseTestServer();
            
        return builder;
    }
}
```

I use `user secrets` with a connection string for local development, pointing to an instance of SQL Express that runs in Docker. However, when the test project runs in CI/CD pipeline, it uses a connection string defined in `appsettings.Staging.json` and connects with a remote test database.

Class `CustomWebApplicationFactory` requires the `Startup` class to configure necessary services. Thus test project has its own `TestStartup.cs`. We register only essential services.

Note: before integration tests can run, the test database must be already up and running.

## CQRS pattern

The project uses a CQRS architectural pattern with no event sourcing (changes to the application state are **not** stored as a sequence of events). I used the MediatR library (mediator pattern) with the handler template.

The file `RequestHandler.cs` presented below allow easy registration (mapping the handlers).

```csharp
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly DatabaseContext DatabaseContext;
    protected readonly ILoggerService LoggerService;

    protected TemplateHandler(DatabaseContext databaseContext, ILoggerService loggerService)
    {
        DatabaseContext = databaseContext;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}
```

To configure it, in `Dependencies.cs` (registered at startup), we invoke:

```csharp
private static void SetupMediatR(IServiceCollection services) 
{
    services.AddMediatR(options => options.AsScoped(), 
        typeof(Backend.Cqrs.RequestHandler<IRequest, Unit>).GetTypeInfo().Assembly);

    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(HttpRequestBehaviour<,>));
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TokenCheckBehaviour<,>));
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));
}
```

We register behaviours as scoped services. Depending on needs, we can register events before/after handler execution. Example being:

`LoggingBehaviour.cs`:

```csharp
public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
{
    _loggerService.LogInformation($"Begin: Handle {typeof(TRequest).Name}");
    var response = await next();
    _loggerService.LogInformation($"Finish: Handle {typeof(TResponse).Name}");
    return response;
}
```

Logging is part of the middleware pipeline, we log info before and after handler execution.

`FluentValidationBehavior.cs`:

```csharp
public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
{
    if (_validator == null) return next();

    var validationContext = new ValidationContext<TRequest>(request);
    var validationResults = _validator.Validate(validationContext);

    if (!validationResults.IsValid)
        throw new Exceptions.ValidationException(validationResults);

    return next();
}
```

Validator is registered within the middleware pipeline, and if it exists (not null), then we execute it and raise an exception if invalid, otherwise we proceed. Note: `ValidationException.cs` inherits from `BusinessException.cs` which inherits form `System.Exception` (I try not to use `System.Exception` directly).

Such setup allow to have very thin controllers, example endpoint:

```csharp
[HttpGet]
public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool isPublished = true) 
    => await Mediator.Send(new GetAllArticlesQuery { IsPublished = isPublished });
```

When we call `GetAllArticles` endpoint, it sends `GetAllArticlesQuery` request with given parameters. The appropriate handler is `GetAllArticlesQueryHandler`:

```csharp
public class GetAllArticlesQueryHandler : TemplateHandler<GetAllArticlesQuery, IEnumerable<GetAllArticlesQueryResult>>
{
    public GetAllArticlesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<IEnumerable<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Articles
            .AsNoTracking()
            .Where(articles => articles.IsPublished == request.IsPublished)
            .Select(articles => new GetAllArticlesQueryResult 
            { 
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                ReadCount = articles.ReadCount,
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt
            })
            .OrderByDescending(articles => articles.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
```

## Exception Handler

After adding custom exception handler to the middleware pipeline:

```csharp
builder.UseMiddleware<Exceptions>();
```

It will catch exceptions and sets HTTP status. Thus, if we throw an error (business or validation, etc.) manually in the handler, the response is appropriately set up.

```csharp
public async Task InvokeAsync(HttpContext httpContext)
{
    try
    {
        await _requestDelegate(httpContext);
    }
    catch (ValidationException validationException)
    {
        var applicationError = new ApplicationError(validationException.ErrorCode, validationException.Message, validationException.ValidationResult);
        await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.BadRequest).ConfigureAwait(false);
    }
    catch (AuthorizationException authenticationException)
    {
        var innerMessage = authenticationException.InnerException?.Message;
        var applicationError = new ApplicationError(authenticationException.ErrorCode, authenticationException.Message, innerMessage);
        await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.Unauthorized).ConfigureAwait(false);
    }
    catch (AccessException authorizationException)
    {
        var innerMessage = authorizationException.InnerException?.Message;
        var applicationError = new ApplicationError(authorizationException.ErrorCode, authorizationException.Message, innerMessage);
        await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.Forbidden).ConfigureAwait(false);
    }
    catch (BusinessException businessException)
    {
        var innerMessage = businessException.InnerException?.Message;
        var applicationError = new ApplicationError(businessException.ErrorCode, businessException.Message, innerMessage);
        await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.UnprocessableEntity).ConfigureAwait(false);
    }
    catch (Exception exception)
    {
        var innerMessage = exception.InnerException?.Message;
        var applicationError = new ApplicationError(nameof(ErrorCodes.ERROR_UNEXPECTED), exception.Message, innerMessage);
        await WriteErrorResponse(httpContext, applicationError, HttpStatusCode.InternalServerError).ConfigureAwait(false);
    }
}
```

Please note that handlers usually contain manual exceptions other than validation exceptions. They rarely have validation exceptions, typically raised by the `FluentValidation` before the handler is invoked, an example being:

```csharp
public class RemoveArticleCommandHandler : RequestHandler<RemoveArticleCommand, Unit>
{
    public RemoveArticleCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var currentArticle = await DatabaseContext.Articles
            .Where(articles => articles.Id == request.Id)
            .ToListAsync(cancellationToken);

        if (!currentArticle.Any())
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        DatabaseContext.Articles.Remove(currentArticle.Single());
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}
```

These business exceptions (`ARTICLE_DOES_NOT_EXISTS`) shall never be an validation error. Furthermore, it is unlikely that we would want to perform database requests during validation. The validator is responsible for ensuring that input data is valid (not for checking available articles etc.).

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

EF Core will create all the necessary tables and seed test data. More on migrations here: [TokanPages.Backend.Database](https://github.com/TomaszKandula/TokanPages/tree/master/TokanPages.Backend/TokanPages.Backend.Database).

## Client-App

To run client application in development environment, simply execute script `__testrun.sh`. It requires to have Docker installed on local machine.

```shell
APP_VERSION="0.0.1-local-dev"
BUILD_TIMESTAMP=$(date +"%Y-%m-%d at %T")
ALLOWED_ORIGINS="http://localnode:5000/;"
APP_FRONTEND="http://localhost:3000"
APP_BACKEND="http://localhost:5000"
SONAR_TOKEN="<token>"
SONAR_KEY="<key>"
SONAR_HOST="<host>"

docker build . \
  --build-arg "APP_VERSION=$APP_VERSION" \
  --build-arg "APP_DATE_TIME=$BUILD_TIMESTAMP" \
  --build-arg "APP_FRONTEND=$APP_FRONTEND" \
  --build-arg "APP_BACKEND=$APP_BACKEND" \
  --build-arg "ALLOWED_ORIGINS=$ALLOWED_ORIGINS" \
  --build-arg "SONAR_TOKEN=$SONAR_TOKEN" \
  --build-arg "SONAR_KEY=$SONAR_KEY" \
  --build-arg "SONAR_HOST=$SONAR_HOST" \
  -t nginx-clientapp

MACHINE_IP=$(ifconfig en0 | grep inet | grep -v inet6 | awk '{print $2}')

docker run \
  --add-host localnode:"$MACHINE_IP" \
  --rm -it -p 3000:80 nginx-clientapp
```

Note: we must use machine IP address to be able to request localhost that runs backend on port 5000.

We simply build and run docker image defined in the `dockerfile`:

```
# 1 - Build ClientApp
FROM node:15 AS node
WORKDIR /app

COPY ./*.* ./
COPY ./public ./public
COPY ./src ./src

ARG APP_VERSION
ARG APP_DATE_TIME
ARG APP_FRONTEND
ARG APP_BACKEND
ARG SONAR_TOKEN
ARG SONAR_KEY
ARG SONAR_HOST

ENV REACT_APP_VERSION_NUMBER=${APP_VERSION}
ENV REACT_APP_VERSION_DATE_TIME=${APP_DATE_TIME}
ENV REACT_APP_FRONTEND=${APP_FRONTEND}
ENV REACT_APP_BACKEND=${APP_BACKEND}

RUN if [ !-z $SONAR_TOKEN ] || [ !-z $SONAR_KEY ] || [ !-z $SONAR_HOST ]; \
then yarn install && yarn app-test --ci --coverage && yarn build; \
else yarn install && yarn global add sonarqube-scanner && yarn app-test --ci --coverage \
&& yarn sonar -Dsonar.login=${SONAR_TOKEN} -Dsonar.projectKey=${SONAR_KEY} -Dsonar.host.url=${SONAR_HOST} \
&& yarn build; fi

# 2 - Build NGINX 
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

ARG ALLOWED_ORIGINS
ENV PROXY_PASS=${ALLOWED_ORIGINS}
COPY ./nginx/nginx.template /etc/nginx/nginx.template

RUN rm -rf ./* && apk update && apk add --no-cache bash
COPY --from=node /app/build .
CMD /bin/bash -c "envsubst '\$PROXY_PASS' < /etc/nginx/nginx.template > /etc/nginx/nginx.conf && nginx -g 'daemon off;'"
```

We run tests and build react app with given environmental variables. Please note that we also run SonarQube scanner if variables are supplied.

React application is then running on NGINX web server that uses pre-configured file that only takes proxy url. Please note that because we use Alpine Linux O/S, we separately add APK to add bash. Alpine consumes roughly 35 MB of disk space.

## CI/CD

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build_test.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test.yml) - it builds .NET Core application and React application, then runs all the available tests; finally, it scans the code with SonarQube.
1. [stage_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/stage_build_test_publish.yml) - it builds, tests and publishes Docker image to the staging Azure WebApp.
1. [master_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build_test_publish.yml) - it builds, tests and publishes Docker image to the production Azure WebApp.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).