### Migration Manual
---

Make sure you have local database setup and all the connection strings are in place. 
When change has been introduced, invoke:

1. `Add-Migration -StartupProject TokanPages -Project TokanPages.Backend.Database -Context DatabaseContext -OutputDir Migrations <name>`
1. `Update-Database -StartupProject TokanPages -Project TokanPages.Backend.Database -Context DatabaseContext`

To remove migration:

1. `Remove-Migration -StartupProject TokanPages -Project TokanPages.Backend.Database -Context DatabaseContext`

When updating model, remove migration first, make changes and add new migration again. If `update-migration` has been already invoked based on 
previous migration on local branch and we need to remove such migration, it is better to run `update-database` with migration name before changes.
In such case EF Core will perform downgrade database then we can perform `remove-migration`.

Important: do not modify manually migrations and auto-generated scripts by EF Core.
