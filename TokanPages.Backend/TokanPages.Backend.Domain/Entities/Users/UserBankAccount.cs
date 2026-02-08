using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserBankAccounts")]
public class UserBankAccount : Entity<Guid>
{
    public Guid UserId { get; set; }

    public string BankName { get; set; }

    public string SwiftNumber { get; set; }        

    public string AccountNumber { get; set; }

    public CurrencyCode CurrencyCode { get; set; }
}