using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserMessagesCache")]
public class UserMessageCache : Entity<Guid>
{
    [Required]
    public string ChatKey { get; set; }
    [Required]
    public string Notification { get; set; }
}