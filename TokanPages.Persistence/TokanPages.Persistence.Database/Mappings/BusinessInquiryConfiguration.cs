using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class BusinessInquiryConfiguration : IEntityTypeConfiguration<BusinessInquiry>
{
    public void Configure(EntityTypeBuilder<BusinessInquiry> builder) 
        => builder.Property(inquiry => inquiry.Id).ValueGeneratedOnAdd();
}