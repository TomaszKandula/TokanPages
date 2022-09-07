using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission6
{
    public static readonly Guid Id = Guid.Parse("301fa9f6-f104-41ac-b8cf-49623de01937");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanSelectComments);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}