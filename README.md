# Personal Website

Website exposing my programming interests (mostly Microsoft technologies) mainly through various articles that can be published - currently, only by admin - but once text editor is added along with user account capabilities, others will be able to entertain write-ups too. Commenting and voting capabilities are also planned.

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
1. Swagger-UI.

Unit Tests and Integration Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

Project is dockerized and deployed via GitHub Actions to Azure App Service that uses Container Registry.

## Project structure

Application is split into:

1. Backend.
1. TokanPages.
1. Tests.

Backend holds various services used by TokanPages where main application resides with its ClientApp part (compiled separately). Tests holds tests helpers and projects for Integration Tests (focus to examine http client) and Unit Tests (focus to examine validators and handlers).

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

## Integration Tests

Focuses on testing HTTP responses, dependencies and theirs configuration.

## Unit Tests

Covers all the logic used in the controllers (please note that the endpoints does not provide any business logic, they are only responsible for handling requests etc.). All dependencies are mocked/faked. For mocking [Moq](https://github.com/moq/moq4) and [MockQueryable.Moq](https://github.com/romantitov/MockQueryable) have been used.

## CI/CD

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build_test.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test.yml) - it builds .NET Core application and React application in Docker, then runs all the available tests (Frontend and Backend). Each PR will invoke this action.
1. [dev_build_test_push.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build_test_push.yml) - it builds and tests both backend and frontend along with an Docker image so it can be later manually uploaded. 
1. [master_build_test_publish.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build_test_publish.yml) - it builds, tests and publish Docker image to the Azure WebApp.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
