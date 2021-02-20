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

Test are provided, but there is no full coverage yet.

### Back-end

1. WebAPI (NET Core 3.1 / C#).
1. Azure SQL Database.
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

For testing, local SQL server/database is used, connection string have to be setup by replacing __set_env__ with proper value:

```
{
  "ConnectionStrings": 
  {
    "DbConnect": "set_env"
  }
}
```

Go to Package Manager Console (PMC) to execute following command:

`Update-Database -StartupProject TokanPages -Project TokanPages.Backend.Database -Context DatabaseContext`

EF Core will create all the necessary tables and will seed test data. More on migrations here: [TokanPages.Backend.Database](https://github.com/TomaszKandula/TokanPages/tree/dev/Backend/TokanPages.Backend.Database).

## Integration Tests

Focuses on testing HTTP responses, dependencies and theirs configuration.

## Unit Tests

Covers all the logic used in the controllers (please note that the endpoints does not provide any business logic, they are only responsible for handling requests etc.). All dependencies are mocked/faked. For mocking [Moq](https://github.com/moq/moq4) and [MockQueryable.Moq](https://github.com/romantitov/MockQueryable) have been used.

## CI/CD

CI/CD is done via GitHub actions. Currentl only one pipeline is added and all the tests runs inside Docker the container.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
