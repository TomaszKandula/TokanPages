using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class CreateUserPaymentDto : UserPaymentBaseDto
{
    public Guid? Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CreatedBy { get; set; }
}