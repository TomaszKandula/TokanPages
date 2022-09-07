namespace TokanPages.Persistence.Database.Initializer.Data.Roles;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Role3
{
    public static readonly Guid Id = Guid.Parse("ece95a5a-e6fd-414a-9a2a-62658c8bc11e");

    public const string Name = nameof(Backend.Domain.Enums.Roles.ArticlePublisher);

    public const string Description = "User can publish articles";

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-11-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}