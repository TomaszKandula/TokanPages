using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission2
{
    public static readonly Guid Id = Guid.Parse("070483c4-98cb-4b2a-be47-6c85c96854ba");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanInsertArticles);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}