using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Permissions;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Seeders;

[ExcludeFromCodeCoverage]
public static class PermissionsSeeder
{
    public static IEnumerable<Permissions> SeedPermissions()
    {
        return new List<Permissions>
        {
            new()
            {
                Id = Permission1.Id, 
                Name = Permission1.Name,
                CreatedAt = Permission1.CreatedAt,
                CreatedBy = Permission1.CreatedBy,
                ModifiedAt = Permission1.ModifiedAt,
                ModifiedBy = Permission1.ModifiedBy
            },
            new() 
            { 
                Id = Permission2.Id, 
                Name = Permission2.Name,
                CreatedAt = Permission2.CreatedAt,
                CreatedBy = Permission2.CreatedBy,
                ModifiedAt = Permission2.ModifiedAt,
                ModifiedBy = Permission2.ModifiedBy
            },
            new()
            {
                Id = Permission3.Id, 
                Name = Permission3.Name,
                CreatedAt = Permission3.CreatedAt,
                CreatedBy = Permission3.CreatedBy,
                ModifiedAt = Permission3.ModifiedAt,
                ModifiedBy = Permission3.ModifiedBy
            },
            new()
            {
                Id = Permission4.Id, 
                Name = Permission4.Name,
                CreatedAt = Permission4.CreatedAt,
                CreatedBy = Permission4.CreatedBy,
                ModifiedAt = Permission4.ModifiedAt,
                ModifiedBy = Permission4.ModifiedBy
            },
            new()
            {
                Id = Permission5.Id, 
                Name = Permission5.Name,
                CreatedAt = Permission5.CreatedAt,
                CreatedBy = Permission5.CreatedBy,
                ModifiedAt = Permission5.ModifiedAt,
                ModifiedBy = Permission5.ModifiedBy
            },
            new()
            {
                Id = Permission6.Id, 
                Name = Permission6.Name,
                CreatedAt = Permission6.CreatedAt,
                CreatedBy = Permission6.CreatedBy,
                ModifiedAt = Permission6.ModifiedAt,
                ModifiedBy = Permission6.ModifiedBy
            },
            new()
            {
                Id = Permission7.Id, 
                Name = Permission7.Name,
                CreatedAt = Permission7.CreatedAt,
                CreatedBy = Permission7.CreatedBy,
                ModifiedAt = Permission7.ModifiedAt,
                ModifiedBy = Permission7.ModifiedBy
            },
            new()
            {
                Id = Permission8.Id, 
                Name = Permission8.Name,
                CreatedAt = Permission8.CreatedAt,
                CreatedBy = Permission8.CreatedBy,
                ModifiedAt = Permission8.ModifiedAt,
                ModifiedBy = Permission8.ModifiedBy
            },
            new()
            {
                Id = Permission9.Id, 
                Name = Permission9.Name,
                CreatedAt = Permission9.CreatedAt,
                CreatedBy = Permission9.CreatedBy,
                ModifiedAt = Permission9.ModifiedAt,
                ModifiedBy = Permission9.ModifiedBy
            },
            new()
            {
                Id = Permission10.Id, 
                Name = Permission10.Name,
                CreatedAt = Permission10.CreatedAt,
                CreatedBy = Permission10.CreatedBy,
                ModifiedAt = Permission10.ModifiedAt,
                ModifiedBy = Permission10.ModifiedBy
            },
            new()
            {
                Id = Permission11.Id, 
                Name = Permission11.Name,
                CreatedAt = Permission11.CreatedAt,
                CreatedBy = Permission11.CreatedBy,
                ModifiedAt = Permission11.ModifiedAt,
                ModifiedBy = Permission11.ModifiedBy
            },
            new()
            {
                Id = Permission12.Id, 
                Name = Permission12.Name,
                CreatedAt = Permission12.CreatedAt,
                CreatedBy = Permission12.CreatedBy,
                ModifiedAt = Permission12.ModifiedAt,
                ModifiedBy = Permission12.ModifiedBy
            },
            new()
            {
                Id = Permission13.Id, 
                Name = Permission13.Name,
                CreatedAt = Permission13.CreatedAt,
                CreatedBy = Permission13.CreatedBy,
                ModifiedAt = Permission13.ModifiedAt,
                ModifiedBy = Permission13.ModifiedBy
            },
            new()
            {
                Id = Permission14.Id, 
                Name = Permission14.Name,
                CreatedAt = Permission14.CreatedAt,
                CreatedBy = Permission14.CreatedBy,
                ModifiedAt = Permission14.ModifiedAt,
                ModifiedBy = Permission14.ModifiedBy
            },
            new()
            {
                Id = Permission15.Id, 
                Name = Permission15.Name,
                CreatedAt = Permission15.CreatedAt,
                CreatedBy = Permission15.CreatedBy,
                ModifiedAt = Permission15.ModifiedAt,
                ModifiedBy = Permission15.ModifiedBy
            },
            new()
            {
                Id = Permission16.Id, 
                Name = Permission16.Name,
                CreatedAt = Permission16.CreatedAt,
                CreatedBy = Permission16.CreatedBy,
                ModifiedAt = Permission16.ModifiedAt,
                ModifiedBy = Permission16.ModifiedBy
            },
            new()
            {
                Id = Permission17.Id, 
                Name = Permission17.Name,
                CreatedAt = Permission17.CreatedAt,
                CreatedBy = Permission17.CreatedBy,
                ModifiedAt = Permission17.ModifiedAt,
                ModifiedBy = Permission17.ModifiedBy
            }
        };
    }
}