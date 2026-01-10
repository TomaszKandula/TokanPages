using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class ServiceBusMessageConfiguration : IEntityTypeConfiguration<ServiceBusMessage>
{
    public void Configure(EntityTypeBuilder<ServiceBusMessage> builder) 
        => builder.Property(message => message.Id).ValueGeneratedOnAdd();
}