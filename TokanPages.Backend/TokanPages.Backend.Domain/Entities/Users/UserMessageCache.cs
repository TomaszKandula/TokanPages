using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserMessagesCache")]
public class UserMessageCache : Entity<Guid>
{
    public required string ChatKey { get; set; }

    public required string Notification { get; set; }
}