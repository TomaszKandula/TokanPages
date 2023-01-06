## Migration Runner

### Basics

The Continuous Delivery pipeline and End-To-End tests use in this project. It uses one of the configuration files depending on the current environment. Therefore, an environment variable `ASPNETCORE_ENVIRONMENT` must be set in the pipeline. If this variable is not present, then the program will always fall back to a TESTING environment.

### Usage

Navigate to the `net6.0` folder and run one of the following commands:

1. `dotnet TokanPages.Persistence.MigrationRunner.dll --migrate` - use it to migrate the current database context.
1. `dotnet TokanPages.Persistence.MigrationRunner.dll --seed` - use it to seed the test data.
1. `dotnet TokanPages.Persistence.MigrationRunner.dll --migrate-seed` - use it to migrate and seed the test data.
1. `dotnet TokanPages.Persistence.MigrationRunner.dll --next-prod` - use it to apply the migration to a new production database. Data from a current production database will be copied over. WARNING: it requires SQL migration script. If no script is present, then database migration is skipped. Script name must always comply with the following convention: `<VERSION>_<CONTEXT_NAME>_ToProd.sql`. A migration script should always be added when the database schema changes or when we change how we save the data in the database.

Passing no option will display the welcome/help screen.

### Environment variable

To dynamically apply an environment variable on macOS and Linux, please use the following:

`ASPNETCORE_ENVIRONMENT=Staging dotnet <arguments>`.
