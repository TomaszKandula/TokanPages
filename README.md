## TokanPages
### Microsoft technologies and more

TokanPages is a project repository that holds my web page to share my programming interests (among others), primarily Microsoft technologies. 

The backend architecture of the solution follows the modular-monolith concept. Everything is split into projects and services with only one underlying SQL database. The frontend solution is made with React w/Redux. Everything is dockerized and hosted on the VPS.

Despite mainly focusing on software development and Microsoft technologies, this web page aims to be more than just a simple personal home site.

## CI/CD Pipelines

[![Build and run tests (dev)](https://github.com/TomaszKandula/TokanPages/actions/workflows/dev_build.yml/badge.svg)](https://github.com/TomaszKandula/TokanPages/actions/workflows/dev_build_test.yml)

## Project metrics
### Frontend

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

### Backend

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

### Frontend

1. React w/Redux (TypeScript).
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

The client app uses React Hooks. Tests are provided using JEST, but there has yet to be full coverage.

The project is dockerized and deployed via GitHub Actions to VPS. The web server of choice is NGINX.

[//]: # (<img alt="" src="https://sonarproxy.tomkandula.com/api/v1/Metrics/Quality?Project=tokanpages-frontend&kill_cache=1">)

### Backend

1. WebApi (NET 6, C#).
1. Microsoft Windows Server 2022 w/SQL Database.
1. Entity Framework Core.
1. Azure Blob Storage.
1. Azure Redis Cache.
1. Azure Key Vault. 
1. Azure Service Bus.
1. MediatR library.
1. CQRS pattern (with no event sourcing).
1. FluentValidation.
1. SeriLog.
1. Swagger-UI.
1. Polly.
1. SignalR.

Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

The project is dockerized and deployed to the VPS.

[//]: # (<img alt="" src="https://sonarproxy.tomkandula.com/api/v1/Metrics/Quality?Project=tokanpages-backend&kill_cache=1">)

## Continuous Integration / Continuous Delivery

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build.yml) - it builds .NET Core application and React application, then runs all the available tests; finally, it scans the code with SonarQube.
1. [master_build.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build.yml) - it scans the repository, generate release number and pushes the code to the server for installation.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
