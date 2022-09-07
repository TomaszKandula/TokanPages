using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class HttpRequestsConfiguration : IEntityTypeConfiguration<HttpRequests>
{
    public void Configure(EntityTypeBuilder<HttpRequests> builder) 
        => builder.Property(httpRequests => httpRequests.Id).ValueGeneratedOnAdd();
}