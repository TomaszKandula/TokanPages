using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserBankAccounts")]
public class UserBankAccount : Entity<Guid>
{
    public required Guid UserId { get; set; }

    public required string BankName { get; set; }

    public required string SwiftNumber { get; set; }        

    public required string AccountNumber { get; set; }

    public required CurrencyCode CurrencyCode { get; set; }
}