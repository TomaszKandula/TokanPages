## Migration Runner

### Basics

This project will be used by CD pipeline to update given database. However, for local development it may be manually run to setup local database.

Please note, that there ought to be an environment variable `ASPNETCORE_ENVIRONMENT` set in the pipeline. If no variable is present, then program will fallback to TESTING environment.

### Usage

Navigate to the `net6.0` folder and run the following command:

`dotnet TokanPages.Persistence.MigrationRunner.dll`

or

`TokanPages.Persistence.MigrationRunner.exe`.