## Migration Runner

### Basics

This project will be used by CD pipeline to update given database. However, for local development it may be manually used to setup a local database.

Please note, that there ought to be an environment variable `ASPNETCORE_ENVIRONMENT` set in the pipeline. If no variable is present, then program will fallback to TESTING environment.

### Usage

Navigate to the `net6.0` folder and run one of the following commands:

`dotnet TokanPages.Persistence.MigrationRunner.dll --migrate` - use it to migrate current database context.

`dotnet TokanPages.Persistence.MigrationRunner.dll --seed` - use it to seed the test data.

`dotnet TokanPages.Persistence.MigrationRunner.dll --migrate-seed` - use it to migrate and seed the test data.

`dotnet TokanPages.Persistence.MigrationRunner.dll --next-prod` - use it to apply migration to a new production database. Data from a current production database will be copy over.

Passing no option will display help screen.

### Environment variable

To apply environment variable, on macOS and Linux use:

`ASPNETCORE_ENVIRONMENT=Staging`

before `dotnet` command.
