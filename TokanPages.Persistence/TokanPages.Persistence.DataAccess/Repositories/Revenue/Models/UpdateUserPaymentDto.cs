using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserPaymentDto : UserPaymentBaseDto
{
    public Guid ModifiedBy { get; set; }
}