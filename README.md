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
    <img alt="lines" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=ncloc&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="code smells" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=code_smells&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="bugs" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="rating" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="security rating" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=security_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="reliability rating" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="debt" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=sqale_index&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="duplicate" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=duplicated_lines_density&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="coverage" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-frontend&metric=coverage&kill_cache=1">
  </a>
</p>

### Backend

<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="lines" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=ncloc&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="code smells" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=code_smells&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="bugs" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=bugs&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="rating" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=sqale_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="security rating" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=security_rating&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="reliability rating" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=reliability_rating&kill_cache=1">
  </a>
</p>
<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="debt" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=sqale_index&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="duplicate" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=duplicated_lines_density&kill_cache=1">
  </a>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="coverage" src="https://tomkandula.com/api/v1.0/content/metrics/getMetrics?project=tokanpages-backend&metric=coverage&kill_cache=1">
  </a>
</p>

## Tech-Stack

### Frontend

<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="lines" src="https://tomkandula.com/api/v1.0/content/metrics/getQualityGate?project=tokanpages-frontend&kill_cache=1">
  </a>
</p>

1. React w/Redux (TypeScript).
2. Material-UI.
3. JEST.
4. Axios.
5. AOS.
6. Validate.js.
7. Syntax Highlighter.
8. Emoji Render.
9. HTML Parser.
10. Husky.
11. Semantic-Release.
12. NGINX.

The client app uses React Hooks. Tests are provided using JEST, but there has yet to be full coverage.

The project is dockerized and deployed via GitHub Actions to VPS. The web server of choice is NGINX.

[//]: # (<img alt="" src="https://tomkandula.com/api/v1.0/metrics/getQualityGate?Project=tokanpages-frontend&kill_cache=1">)

### Backend

<p>
  <a href="https://sonarqube.tomkandula.com">
    <img alt="lines" src="https://tomkandula.com/api/v1.0/content/metrics/getQualityGate?project=tokanpages-backend&kill_cache=1">
  </a>
</p>

1. WebApi (NET 6, C#).
2. MS Windows Server 2022 w/SQL Database (deployed to the separate VPS).
3. Redis Cache (deployed to the separate VPS).
4. Entity Framework Core.
5. Azure Blob Storage.
6. Azure Key Vault. 
7. Azure Service Bus.
8. MediatR library.
9. CQRS pattern.
10. FluentValidation.
11. SeriLog.
12. Swagger-UI.
13. Polly.
14. SignalR.
15. PuppeteerSharp (Headless Chrome).
16. PayU API (to be changed to Revolut API).

Tests are provided using [XUnit](https://github.com/xunit/xunit) and [FluentAssertions](https://github.com/fluentassertions/fluentassertions).

The project is dockerized and deployed to the VPS.

[//]: # (<img alt="" src="https://tomkandula.com/api/v1.0/metrics/getQualityGate?Project=tokanpages-backend&kill_cache=1">)

## Continuous Integration / Continuous Delivery

CI/CD is done via GitHub actions. There are three scripts:

1. [dev_build.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/dev_build.yml) - it builds .NET Core application and React application, then runs all the available tests; finally, it scans the code with SonarQube.
1. [master_build.yml](https://github.com/TomaszKandula/TokanPages/blob/dev/.github/workflows/master_build.yml) - it scans the repository, generate release number and pushes the code to the server for installation.

## End Note

This project is under active development, the status can be monitored here: [Board](https://github.com/users/TomaszKandula/projects/7).
