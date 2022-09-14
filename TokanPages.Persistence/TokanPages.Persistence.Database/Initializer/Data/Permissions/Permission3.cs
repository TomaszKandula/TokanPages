using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission3
{
    public static readonly Guid Id = Guid.Parse("80db7f7c-9ac1-446a-8a18-3ec3750b9929");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanUpdateArticles);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}