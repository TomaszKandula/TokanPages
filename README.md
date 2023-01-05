## TokanPages
### Microsoft technologies and more

TokanPages is the repository that holds my web page to share my programming interests (among others), primarily Microsoft technologies. It is so called modular-monolith, split into services. There is only one underlying SQL database.

The web-page itself aim to be more than just being a simple personal web page. 

## CI/CD Pipelines

[![Build & run tests (dev)](https://github.com/TomaszKandula/TokanPages/actions/workflows/dev_build_test.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/dev_build_test.yml)

[![Build, test and publish (stage)](https://github.com/TomaszKandula/TokanPages/actions/workflows/stage_build_test_publish.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/stage_build_test_publish.yml)

[![Build, test and publish (master)](https://github.com/TomaszKandula/TokanPages/actions/workflows/master_build_test_publish.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/master_build_test_publish.yml)

## Project metrics
### Client-App

<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=ncloc&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=code_smells&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=security_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=sqale_index&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=duplicated_lines_density&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-frontend&metric=coverage&kill_cache=1">
  </a>
</p>

### WebApi / Backend

<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=ncloc&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=code_smells&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=security_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=sqale_index&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=duplicated_lines_density&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="" src="https://sonarproxy.tomkandula.com/api/v1/metrics?project=tokanpages-backend&metric=coverage&kill_cache=1">
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

<img alt="" src="https://sonarproxy.tomkandula.com/api/v1/Metrics/Quality?Project=tokanpages-frontend&kill_cache=1">

### Back-end

1. WebApi (NET 6, C#).
1. Microsoft Windows Server 2022 w/SQL Database.
1. Entity Framework Core.
1. Azure Blob Storage.
1. Azure Redis Cache.
1. MediatR library.
1. CQRS pattern with no event sourcing.
1. FluentValidation.
1. SeriLog.
1. Swagger-UI.
1. Polly.
1. SignalR.

Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

Project is dockerized and deployed via GitHub Actions to Azure App Service that uses Container Registry.

<img alt="" src="https://sonarproxy.tomkandula.com/api/v1/Metrics/Quality?Project=tokanpages-backend&kill_cache=1">

## Project structure

_TokanPages.ClientApp_

| Folder | Description             |
|--------|-------------------------|
| nginx  | WebServer configuration |
| public | WebApp entrypoint       |
| src    | Frontend in React       |

React application runs on NGINX in Docker. It is deployed on the main domain.

Unit tests for the frontend are provided; use command `yarn app-test` to run all tests.

_TokanPages.Backend_

| Folder              | Description                          |
|---------------------|--------------------------------------|
| Backend.Application | Handlers                             |
| Backend.Core        | Reusable core elements               |
| Backend.Domain      | Domain entities, contracts and enums |
| Backend.Shared      | Shared models and resources          |

_TokanPages.Configuration_

| File                        | Description              |
|-----------------------------|--------------------------|
| appsettings.Production.json | Production configuration |
| appsettings.Staging.json    | Staging configuration    |
| appsettings.Testing.json    | Testing configuration    |

.NET configuration files shared across the projects.

_TokanPages.Persistence_

| Folder                                 | Description        |
|----------------------------------------|--------------------|
| TokanPages.Persistence.Caching         | Caching service    |
| TokanPages.Persistence.Database        | Database context   |
| TokanPages.Persistence.MigrationRunner | Database migration |

_TokanPages.Services_

| Folder                       | Description                |
|------------------------------|----------------------------|
| Services.AzureStorageService | Application remote storage |
| Services.BehaviourService    | MediatR pipelines          |
| Services.CipheringService    | Password hashing           |
| Services.EmailSenderService  | Email handling             |
| Services.HttpClientService   | Custom HTTP client         |
| Services.RedisCacheService   | REDIS cache implementation |
| Services.UserService         | User provider              |
| Services.WebTokenService     | JWT handling               |
 | Services.WebSocketService    | WSS handling               |

_TokanPages.WebApi_

| Folder        | Description              |
|---------------|--------------------------|
| Properties    | Lunch settings           |
| Configuration | Application dependencies |
| Controllers   | WebApi                   |
| Middleware    | Custom middleware        |

_TokanPages.WebApi.Dto_

Collection of all DTO models used by the mappers and controllers.

_TokanPages.Tests_

| Folder        | Description                   |
|---------------|-------------------------------|
| EndToEndTests | Http client tests             |
| UnitTests     | Handlers and validators tests |

Unit tests covers handlers and validators. All dependencies are mocked. For mocking [Moq](https://github.com/moq/moq4) has been used.

End-to-End tests focuses on testing HTTP client responses, dependencies and theirs configuration.

To run backend tests, use command `dotnet test`.

## Continuous Integration / Continuous Delivery

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build_test.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test.yml) - it builds .NET Core application and React application, then runs all the available tests; finally, it scans the code with SonarQube.
1. [stage_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/stage_build_test_publish.yml) - it builds, tests and publishes Docker image to the staging Azure WebApp.
1. [master_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build_test_publish.yml) - it builds, tests and publishes Docker image to the production Azure WebApp.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
