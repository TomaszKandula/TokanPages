using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.DataAccess.Mappings;

[ExcludeFromCodeCoverage]
public class HttpRequestConfiguration : IEntityTypeConfiguration<HttpRequest>
{
    public void Configure(EntityTypeBuilder<HttpRequest> builder) 
        => builder.Property(request => request.Id).ValueGeneratedOnAdd();
}