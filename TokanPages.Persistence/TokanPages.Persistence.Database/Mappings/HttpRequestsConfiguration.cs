using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class HttpRequestsConfiguration : IEntityTypeConfiguration<HttpRequest>
{
    public void Configure(EntityTypeBuilder<HttpRequest> builder) 
        => builder.Property(httpRequests => httpRequests.Id).ValueGeneratedOnAdd();
}