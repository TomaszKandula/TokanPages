using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserInformation")]
public class UserInfo : Entity<Guid>, IAuditable
{
    public required Guid UserId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? UserAboutText { get; set; }

    public string? UserImageName { get; set; }

    public string? UserVideoName { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}